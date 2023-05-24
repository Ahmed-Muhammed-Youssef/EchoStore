using Core.Entities.Identity;
using System.Security.Claims;

namespace Core.Interfaces.Services
{
    public interface ICookieService
    {
        ClaimsPrincipal GetPrincipal(AppUser user, string authenticationType);
    }
}
