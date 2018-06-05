using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Synchronized.Domain;
using Synchronized.UI.Utilities;
using Synchronized.UI.Utilities.Interfaces;
using System.Threading.Tasks;

namespace Synchronized.Controllers
{
    public class SynchronizedController: Controller
    {
        protected readonly IDataConverter _dataConverter;
        protected readonly UserManager<ApplicationUser> _userManager;

        public SynchronizedController(
            IDataConverter converter,
            UserManager<ApplicationUser> userManager
        )
        {
            _dataConverter = converter;
            _userManager = userManager;
        }

        protected async Task<string> GetUserIdAsync()
        {
            var userId = await  Utils.GetUserIdAsync(HttpContext, _userManager);
            return userId;
        }

        protected async Task<ApplicationUser> GetUserAsync() => await Utils.GetUserAsync(HttpContext, _userManager);
    }
}
