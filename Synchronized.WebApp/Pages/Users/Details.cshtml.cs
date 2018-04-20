using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.Core.Interfaces;
using Synchronized.Model;

namespace Synchronized.WebApp.Pages.Users
{
    public class DetailsModel : PageModel
    {
        public ApplicationUser ApplicationUser { get; set; }

        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }

        private readonly IUsersService _service;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(
            IUsersService service,
            ILogger<DetailsModel> logger
        )
        {
            _service = service;
            _logger = logger;
            CurrentPage = 1;
        }

        public void OnGet(string id)
        {
            ApplicationUser = _service.FindById(id);
        }
    }
}