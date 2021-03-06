﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Synchronized.Domain;
using System.Threading.Tasks;

namespace Synchronized.UI.Utilities
{
    public class Utils
    {
        public static async Task<string> GetUserIdAsync(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.GetUserAsync(context.User);
            string userId = null;
            userId = user?.Id;
            return userId;
        }

        public static async Task<ApplicationUser> GetUserAsync(HttpContext context, UserManager<ApplicationUser> userManager) => await userManager.GetUserAsync(context.User);
    }
}
