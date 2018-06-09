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
            CurrentPage = pageNumber ?? 1;
            Users = _service.GetIndexPage(CurrentPage);
        }
    }
}