using Core.Entities.Identity;
using Core.Interfaces.Services;
using System.Collections.Generic;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class CookieService : ICookieService
    {
        public ClaimsPrincipal GetPrincipal(AppUser user, string authenticationType)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, user.DisplayName),
                new Claim(ClaimTypes.Email , user.Email)                
            };
            var calimsIdentity = new ClaimsIdentity(claims, authenticationType);

            var prinipal = new ClaimsPrincipal(calimsIdentity);
            return prinipal;
        }
    }
}
