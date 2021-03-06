using Common.Extentions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAspNetMvcGrid.Models;

namespace WebAppAspNetMvcGrid.Controllers
{
    [Authorize]
    public class CitizenshipsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new GosuslugiContext();
            return View(db.Set<Citizenship>());
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var citizenships = new Citizenship();
            return View(citizenships);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(Citizenship model)
        {
            var db = new GosuslugiContext();
            if (model.Key != GetKey())
                ModelState.AddModelError("Key", "Ключ для создания/изменения записи указан не верно");
            if (!ModelState.IsValid)
            {
                var citizenShips = db.Citizenships.ToList();
                ViewBag.Create = model;
                return View("Index", citizenShips);
            }



            db.Citizenships.Add(model);
            db.SaveChanges();

            return RedirectPermanent("/Citizenships/Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            var db = new GosuslugiContext();
            var citizenships = db.Citizenships.FirstOrDefault(x => x.Id == id);
            if (citizenships == null)
                return RedirectPermanent("/Citizenships/Index");

            db.Citizenships.Remove(citizenships);
            db.SaveChanges();

            return RedirectPermanent("/Citizenships/Index");
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var db = new GosuslugiContext();
            var citizenships = db.Citizenships.FirstOrDefault(x => x.Id == id);
            if (citizenships == null)
                return RedirectPermanent("/Citizenships/Index");

            return View(citizenships);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Citizenship model)
        {
            var db = new GosuslugiContext();
            if (model.Key != GetKey())
                ModelState.AddModelError("Key", "Ключ для создания/изменения записи указан не верно");
            var citizenships = db.Citizenships.FirstOrDefault(x => x.Id == model.Id);
            if (citizenships == null)
                ModelState.AddModelError("Id", "Гражданство не найдено");

            if (!ModelState.IsValid)
            {
                var citizenship = db.Citizenships.ToList();
                ViewBag.Create = model;
                return View("Index", citizenship);
            }

            MappingCitizenships(model, citizenships);

            db.Entry(citizenships).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Citizenships/Index");
        }

        private void MappingCitizenships(Citizenship sourse, Citizenship destination)
        {
            destination.Name = sourse.Name;
            destination.Key = sourse.Key;
        }
        private string GetKey()
        {
            var db = new GosuslugiContext();
            var setting = db.Settings.FirstOrDefault(x => x.Type == SettingType.Password);
            if (setting == null)
                throw new Exception("Setting not found");

            return setting.Value;
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult GetXlsx()
        {
            var db = new GosuslugiContext();
            var xlsx = db.Citizenships.ToXlsx();

            return File(xlsx.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Citizenships.xlsx");
        }
    }
}