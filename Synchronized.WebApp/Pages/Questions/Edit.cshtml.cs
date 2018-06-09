using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.Domain;
using Synchronized.ViewModel;
using Synchronized.ViewServices.Interfaces;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Questions
{
    public class EditModel : PageModel
    {
        private IPostsService _service;
        private ILogger<DetailsModel> _logger;
        private UserManager<ApplicationUser> _userManager;

        public EditModel(
            IPostsService service,
            ILogger<DetailsModel> logger,
            UserManager<ApplicationUser> userManager
        )
        {
            _service = service;
            _logger = logger;
            _userManager = userManager;
        }
        [BindProperty]
        public EditViewModel Post { get; set; }

        public async Task OnGetAsync(string id)
        {
            Post = await _service.GetPostForEdit(id);
        }

    }
}