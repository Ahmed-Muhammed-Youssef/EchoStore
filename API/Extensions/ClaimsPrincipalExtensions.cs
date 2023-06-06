﻿using System.Linq;
using System.Security.Claims;

namespace Asp.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
        }
    }
}
