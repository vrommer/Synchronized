using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Synchronized.UI.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Synchronized.Controllers
{
    public class SynchronizedController: Controller
    {
        protected readonly IDataConverter _dataConverter;
        protected readonly UserManager<Domain.ApplicationUser> _userManager;

        public SynchronizedController(
            IDataConverter converter,
            UserManager<Domain.ApplicationUser> userManager
        )
        {
            _dataConverter = converter;
            _userManager = userManager;
        }

        protected async Task<string> GetUserIdAsync()
        {
            string userId = null;
            var user = await GetCurrentUserAsync();
            userId = user?.Id;
            return userId;
        }

        protected Task<Domain.ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
