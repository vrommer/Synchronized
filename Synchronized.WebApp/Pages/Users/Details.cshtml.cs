using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.Core.Interfaces;
using Synchronized.Domain;

namespace Synchronized.WebApp.Pages.Users
{
    public class DetailsModel : PageModel
    {
        public ApplicationUser ApplicationUser { get; set; }

        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }

        private readonly IUsersServiceOld _service;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(
            IUsersServiceOld service,
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