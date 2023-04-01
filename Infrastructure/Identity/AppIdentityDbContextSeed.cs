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
                var user = new AppUser
                {
                    DisplayName = "Klod",
                    Email = "klod@test",
                    UserName = "klod",
                    Address = new Address {FirstName="klod", LastName="mo", Street="12 tele", City="benha", State="QY", Zipcode="19121" }
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
