using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.Core.Interfaces;
using Synchronized.Domain;
using Synchronized.Model;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Questions
{
    public class DetailsModel : PageModel
    {
        public Question Question { get; set; }

        private IQuestionsService _questionsService;
        private readonly ILogger<DetailsModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(
            IQuestionsService questionsService,
            ILogger<DetailsModel> logger,
            UserManager<ApplicationUser> userManager
            )
        {
            _questionsService = questionsService;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task OnGetAsync(string id)
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            Question = _questionsService.FindQuestionById(id);

            if (!Question.QuestionViews.Contains(new QuestionView
            {
                UserId = usr.Id,
                QuestionId = Question.Id
            }))
            {
                // EF Core will not track entity with key
                Question.QuestionViews.Add(new QuestionView {
                    Question = Question,
                    User = usr
                });
            }
            _questionsService.UpdateQuestion(Question);
        }

        [BindProperty]
        public Answer Answer { get; set; }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Question = _questionsService.FindQuestionById(id);
            ApplicationUser usr = await GetCurrentUserAsync();
            Answer.PublisherId = usr.Id;

            Question.Answers.Add(Answer);

            _questionsService.UpdateQuestion(Question);
            return RedirectToPage("/Index");
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}