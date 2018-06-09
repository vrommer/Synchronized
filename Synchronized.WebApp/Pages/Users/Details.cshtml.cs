using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.ViewModel.UsersViewModels;
using Synchronized.ViewServices.Interfaces;

namespace Synchronized.WebApp.Pages.Users
{
    public class DetailsModel : PageModel
    {
        public UserViewModel User { get; set; }

        private readonly IUsersService _service;
        private readonly ILogger<IndexModel> _logger;

        public DetailsModel(
            IUsersService service,
            ILogger<IndexModel> logger
        )
        {
            _service = service;
            _logger = logger;
        }

        //public ApplicationUser ApplicationUser { get; set; }

        //public int CurrentPage { get; set; }
        //public string SearchString { get; set; }
        //public string SortOrder { get; set; }

        //private readonly IUsersServiceOld _service;
        //private readonly ILogger<DetailsModel> _logger;

        //public DetailsModel(
        //    IUsersServiceOld service,
        //    ILogger<DetailsModel> logger
        //)
        //{
        //    _service = service;
        //    _logger = logger;
        //    CurrentPage = 1;
        //}

        //public void OnGet(string id)
        //{
        //    ApplicationUser = _service.FindById(id);
        //}
    }
}