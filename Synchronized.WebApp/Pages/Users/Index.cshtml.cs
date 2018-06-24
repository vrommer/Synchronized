using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel.UsersViewModels;
using Synchronized.ViewServices.Interfaces;
using Synchronized.WebApp.Conventions;

namespace Synchronized.WebApp.Pages.Users
{
    public class IndexModel : PageModel
    {
        public PaginatedList<UserViewModel> Users { get; set; }

        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }

        private const int PAGE_SIZE = 20;
        private readonly IUsersService _service;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            IUsersService service,
            ILogger<IndexModel> logger
        )
        {
            _service = service;
            _logger = logger;
            CurrentPage = 1;
        }

        public void OnGet([MustBeInQueryParameterConvention]int? pageNumber, [MustBeInQueryParameterConvention]string sortOrder = null)
        {
            _logger.LogInformation("Entering Synchronized.WebApp.Pages.Users.Index");
            CurrentPage = pageNumber ?? 1;
            Users = _service.GetIndexPage(CurrentPage);
            foreach (var user in Users)
            {
                _logger.LogDebug("User --->\n\t\tAddress: {0}\n" +
                    "\t\tEmail: {1}\n" +
                    "\t\tName: {2}\n" +
                    "\t\tPoints: {3}", user.Address, user.Email, user.Name, user.Points);
            }            
            _logger.LogInformation("Leaving Synchronized.WebApp.Pages.Users.Index");
        }
    }
}