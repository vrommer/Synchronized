using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel.UsersViewModels;
using Synchronized.ViewServices.Interfaces;
using Synchronized.WebApp.Conventions;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Users
{
    public class IndexModel : PageModel
    {
        public PaginatedList<UserViewModel> Users { get; set; }

        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }
        public string DateSort { get; set; }
        public string NameSort { get; set; }
        public string PointsSort { get; set; }

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

        public async Task OnGetAsync([MustBeInQueryParameterConvention]int? pageNumber, [MustBeInQueryParameterConvention]string sortOrder = null,
            [MustBeInQueryParameterConvention]string searchTerm = null)
        {
            _logger.LogInformation("Entering Synchronized.WebApp.Pages.Users.Index");
            SearchString = searchTerm ?? "";
            SortOrder = sortOrder ?? "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            NameSort = sortOrder == "Nickname" ? "nickname_desc" : "Nickname";
            PointsSort = sortOrder == "Points" ? "points_desc" : "Points";
            CurrentPage = pageNumber ?? 1;
            Users = await _service.GetIndexPage(CurrentPage, sortOrder, SearchString);
            foreach (var user in Users)
            {
                _logger.LogDebug("User --->\n\t\tAddress: {0}\n" +
                    "\t\tEmail: {1}\n" +
                    "\t\tName: {2}\n" +
                    "\t\tRoles: {3}\n" +
                    "\t\tPoints: {4}", user.Address, user.Email, user.Name, user.Roles, user.Points);
            }            
            _logger.LogInformation("Leaving Synchronized.WebApp.Pages.Users.Index");
        }
    }
}