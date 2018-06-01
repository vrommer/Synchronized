using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.ServiceModel;
using Synchronized.ViewModel.QuestionsViewModels;
using Synchronized.ViewServices.Interfaces;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Questions
{
    public class DetailsModel : PageModel
    {
        //public Question Question { get; set; }
        public QuestionForDetailsPage Question { get; set; }

        //private IQuestionsService _questionsService;
        private readonly ILogger<DetailsModel> _logger;
        ILocalService _localService;
        private readonly UserManager<Domain.ApplicationUser> _userManager;

        public DetailsModel(
            //IQuestionsService questionsService,
            ILocalService localService,
            ILogger<DetailsModel> logger,
            UserManager<Domain.ApplicationUser> userManager
            )
        {
            _localService = localService;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task OnGetAsync(string id)
        {
            string userId = null;
            var usr = await GetCurrentUserAsync();
            userId = usr?.Id;
            Question = await _localService.GetQuestionDetailsPageModel(id, userId);
        }

        [BindProperty]
        public Answer Answer { get; set; }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var usr = await GetCurrentUserAsync();

            //Question = _questionsService.FindQuestionById(id);
            //Answer.PublisherId = usr.Id;

            //Question.Answers.Add(Answer);

            //_questionsService.UpdateQuestion(Question);
            return RedirectToPage("/Questions/Details", new { id = id });
        }

        private Task<Domain.ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}