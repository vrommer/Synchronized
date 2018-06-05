using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.Domain;
using Synchronized.UI.Utilities;
using Synchronized.ViewModel.QuestionsViewModels;
using Synchronized.ViewServices.Interfaces;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Questions
{
    public class AskModel : PageModel
    {
        private IQuestionsService _service;
        private ILogger<DetailsModel> _logger;
        private UserManager<ApplicationUser> _userManager;

        public AskModel(
            IQuestionsService service,
            ILogger<DetailsModel> logger,
            UserManager<ApplicationUser> userManager
        )
        {
            _service = service;
            _logger = logger;
            _userManager = userManager;
        }

        [BindProperty]
        public AskViewModel Question { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userId = await Utils.GetUserIdAsync(HttpContext, _userManager);
            
            var questionId = await _service.AskQuestion(Question, userId);
            if (string.IsNullOrEmpty(questionId))
            {
                return RedirectToPage("/Index");
            }
            return RedirectToPage($"/Questions/Details/{questionId}");
        }
    }
}