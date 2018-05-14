using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.Core.Interfaces;
using Synchronized.ServiceModel;
using Synchronized.ViewModel;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Questions
{
    public class DetailsModel : PageModel
    {
        public Question Question { get; set; }
        public DetailsViewModel QuestionViewModel{ get; set; }

        private IQuestionsService _questionsService;
        private readonly ILogger<DetailsModel> _logger;
        private readonly UserManager<Model.ApplicationUser> _userManager;

        public DetailsModel(
            IQuestionsService questionsService,
            ILogger<DetailsModel> logger,
            UserManager<Model.ApplicationUser> userManager
            )
        {
            _questionsService = questionsService;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task OnGetAsync(string id)
        {
            Model.ApplicationUser user = await GetCurrentUserAsync();
            Question = _questionsService.FindQuestionById(id);
            QuestionViewModel = new DetailsViewModel(Question);

            //if ( user!=null && !Question.QuestionViews.Contains(new QuestionView
            //{
            //    UserId = user.Id,
            //    QuestionId = Question.Id
            //}))
            //{
            //    // EF Core will not track entity with key
            //    Question.QuestionViews.Add(new QuestionView {
            //        //Question = Question,
            //        User = user
            //    });
            //    _questionsService.Update(Question);
            //}
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

        private Task<Model.ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}