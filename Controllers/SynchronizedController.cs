using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Synchronized.Domain;
using Synchronized.UI.Utilities;
using Synchronized.UI.Utilities.Interfaces;
using System;
using System.Threading.Tasks;

namespace Synchronized.Controllers
{
    public class SynchronizedController: Controller
    {
        protected readonly IPostsConverter _dataConverter;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly ILogger<Object> _logger;

        public SynchronizedController(
            IPostsConverter converter,
            UserManager<ApplicationUser> userManager,
            ILogger<Object> logger
        )
        {
            _dataConverter = converter;
            _userManager = userManager;
            _logger = logger;
        }

        protected async Task<string> GetUserIdAsync()
        {
            _logger.LogInformation("Starting GetUserIdAsync.");
            var userId = await  Utils.GetUserIdAsync(HttpContext, _userManager);
            _logger.LogInformation("Exiting GetUserIdAsync.");
            return userId;
        }

        protected async Task<ApplicationUser> GetUserAsync() => await Utils.GetUserAsync(HttpContext, _userManager);
    }
}
