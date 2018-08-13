using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.UI.Utilities;
using Synchronized.ViewModel;
using Synchronized.ViewModel.QuestionsViewModels;
using Synchronized.ViewServices.Interfaces;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Questions
{
    public class DetailsModel : PageModel
    {
        public QuestionForDetailsPage Question { get; set; }
        public string UserId { get; set; }

        private readonly ILogger<DetailsModel> _logger;
        IQuestionsService _questionsService;
        private readonly UserManager<Domain.ApplicationUser> _userManager;

        public DetailsModel(
            IQuestionsService questionsService,
            ILogger<DetailsModel> logger,
            UserManager<Domain.ApplicationUser> userManager
        )
        {
            _questionsService = questionsService;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task OnGetAsync(string id)
        {
            UserId = await Utils.GetUserIdAsync(HttpContext, _userManager);
            Question = await _questionsService.GetQuestionDetailsPageModel(id, UserId);
        }

        [BindProperty]
        public AnswerViewModel Answer { get; set; }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userId = await Utils.GetUserIdAsync(HttpContext, _userManager);
            await _questionsService.AnswerQuestion(Answer, userId, id);

            return RedirectToPage("/Questions/Details", new { id = id });
        }
    }
}