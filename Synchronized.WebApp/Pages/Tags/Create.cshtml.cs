using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.Core.Interfaces;
using Synchronized.ServiceModel;
using Synchronized.UI.Utilities;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Tags
{
    public class CreateModel : PageModel
    {
        private readonly ITagsService _tagsService;
        private readonly ILogger<CreateModel> _logger;
        private readonly UserManager<Domain.ApplicationUser> _userManager;

        public CreateModel(ILogger<CreateModel> logger, ITagsService tagsService, UserManager<Domain.ApplicationUser> userManager)
        {
            _logger = logger;
            _tagsService = tagsService;
            _userManager = userManager;
        }

        [BindProperty]
        public Tag Tag{ get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await Utils.GetUserAsync(HttpContext, _userManager);
            var userPoints = user != null ? user.Points : 0;

            var tagId = await _tagsService.CreateTag(Tag, userPoints);

            if (string.IsNullOrWhiteSpace(tagId))
            {
                return RedirectToPage("/Index");
            }
            return RedirectToPage("/Tags/Index");
        }
    }
}