using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Asp.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindUserWithAddressByEmail(this UserManager<AppUser> userManager,string email)
        {
            return await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
