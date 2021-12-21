using System.ComponentModel.DataAnnotations;

namespace WebAppAspNetMvcGrid.Models
{
    public enum SettingType
    {
        [Display(Name = "Пароль")]
        Password = 1,
    }
}