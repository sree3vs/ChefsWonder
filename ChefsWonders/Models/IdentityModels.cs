using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChefsWonders.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("WonderChefsDBConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ChefsWonders.Models.StatusMaster> StatusMasters { get; set; }

        public System.Data.Entity.DbSet<ChefsWonders.Models.Chef> Chefs { get; set; }

        public System.Data.Entity.DbSet<ChefsWonders.Models.FoodType> FoodTypes { get; set; }

        public System.Data.Entity.DbSet<ChefsWonders.Models.FoodCategory> FoodCategories { get; set; }

        public System.Data.Entity.DbSet<ChefsWonders.Models.FoodItem> FoodItems { get; set; }

        public System.Data.Entity.DbSet<ChefsWonders.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<ChefsWonders.Models.OrderDetails> OrderDetails { get; set; }
    }
}