using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.ViewModel.UsersViewModels;
using Synchronized.ViewServices.Interfaces;
using Synchronized.WebApp.Conventions;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Users
{
    public class DetailsModel : PageModel
    {
        public UserViewModel ViewUser { get; set; }

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

        public async Task OnGetAsync([MustBeInQueryParameterConvention]string id)
        {
            ViewUser = await _service.GetDetailsPage(id);
        }
    }
}