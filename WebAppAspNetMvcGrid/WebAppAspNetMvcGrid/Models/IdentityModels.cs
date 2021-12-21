using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebAppAspNetMvcGrid.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class GosuslugiContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<ClientType> ClientTypes { get; set; }
        public DbSet<Citizenship> Citizenships { get; set; }
        public DbSet<AvailableDocument> AvailableDocuments { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<LogHistory> LogHistories { get; set; }
        public GosuslugiContext()
            : base("GosuslugiEntity", throwIfV1Schema: false)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasOptional(x => x.Documents).WithRequired().WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }

        public static GosuslugiContext Create()
        {
            return new GosuslugiContext();
        }
    }
}