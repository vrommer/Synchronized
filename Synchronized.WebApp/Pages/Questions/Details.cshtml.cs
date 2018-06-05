using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.UI.Utilities;
using Synchronized.ViewModel;
using Synchronized.ViewModel.QuestionsViewModels;
using Synchronized.ViewServices.Interfaces;
using System;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Questions
{
    public class DetailsModel : PageModel
    {
        //public Question Question { get; set; }
        public QuestionForDetailsPage Question { get; set; }

        //private IQuestionsService _questionsService;
        private readonly ILogger<DetailsModel> _logger;
        IQuestionsService _questionsService;
        private readonly UserManager<Domain.ApplicationUser> _userManager;

        public DetailsModel(
            //IQuestionsService questionsService,
            IQuestionsService localService,
            ILogger<DetailsModel> logger,
            UserManager<Domain.ApplicationUser> userManager
            )
        {
            _questionsService = localService;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task OnGetAsync(string id)
        {
            var userId = await Utils.GetUserIdAsync(HttpContext, _userManager);
            Question = await _questionsService.GetQuestionDetailsPageModel(id, userId);
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