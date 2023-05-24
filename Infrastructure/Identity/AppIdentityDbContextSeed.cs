using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var admin = new AppUser
                {
                    DisplayName = "admin",
                    Email = "admin@admin.admin",
                    UserName = "admin",
                    Address = new Address {FirstName="admin", LastName="admin", Street="12 test", City="test", State="NYC", Zipcode="19121" }
                };
                var user = new AppUser
                {
                    DisplayName = "user",
                    Email = "user@test",
                    UserName = "user",
                    Address = new Address { FirstName = "user", LastName = "user", Street = "12 test", City = "test", State = "NYC", Zipcode = "19121" }
                };
                await userManager.CreateAsync(user, "P@$$w0rd");
                await userManager.CreateAsync(admin, "P@$$w0rd");
            }
        }
    }
}
