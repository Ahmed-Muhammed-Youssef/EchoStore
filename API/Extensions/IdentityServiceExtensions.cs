using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Asp.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services,
            IConfiguration configuration)
        {
            var builder = services.AddIdentityCore<AppUser>(options =>
            {
                /*options.SignIn.RequireConfirmedAccount = true;*/
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(
                CookieAuthenticationDefaults.AuthenticationScheme,
                options => { 
                    options.Cookie.Name = "logged-in-cookie";
                    options.ExpireTimeSpan = TimeSpan.FromDays(6);
                    options.SlidingExpiration = true;
                }
                );
            return services;
        }
    }
}
