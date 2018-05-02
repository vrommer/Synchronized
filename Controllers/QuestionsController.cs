using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Synchronized.Core.Interfaces;
using Synchronized.Model;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Controllers
{
    [Produces("application/json")]
    public class QuestionsController : Controller
    {
        private IQuestionsService _questionsService;
        private IDataService<CommentedPost> _dataService;

        private readonly ILogger<QuestionsController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionsController(
            IQuestionsService questionsService,
            IDataService<CommentedPost> dataService,
            ILogger<QuestionsController> logger,
            UserManager<ApplicationUser> userManager
            )
        {
            _questionsService = questionsService;
            _dataService = dataService;
            _logger = logger;
            _userManager = userManager;
        }

        // POST: /api/Questions/VoteUpQuestion
        [HttpPost]
        public IActionResult VoteUpQuestion([FromBody]Question question)
        {
            question.Points++;
            _questionsService.UpdateQuestion(question);
            return new ObjectResult(question);
        }

        // POST: api/Voter/VoteDownQuestion
        [HttpPost]
        public IActionResult VoteDownQuestion([FromBody]Question question)
        {
            question.Points--;
            _questionsService.UpdateQuestion(question);
            return new ObjectResult(question);
        }

        // POST: api/Voter/VoteUpAnswer
        [HttpPost]
        public IActionResult VoteUpAnswer([FromBody]Answer answer)
        {
            answer.Points++;
            _questionsService.UpdateAnswer(answer);
            return new ObjectResult(answer);
        }

        // POST: api/Voter/VoteDownAnswer
        [HttpPost]
        public IActionResult VoteDownAnswer([FromBody]Answer answer)
        {
            answer.Points--;
            _questionsService.UpdateAnswer(answer);
            return new ObjectResult(answer);
        }

        // POST: api/Voter/SubmitQuestionComment
        [HttpPost]
        public async Task<IActionResult> SubmitQuestionComment([FromBody]Comment comment)
        {
            Question question = _questionsService.FindQuestionById(comment.PostId);
            ApplicationUser usr = await GetCurrentUserAsync();
            comment.PublisherId = usr.Id;
            question.Comments.Add(comment);
            _questionsService.UpdateQuestion(question);            
            return new ObjectResult(comment);
        }

        // POST: api/Voter/SubmitQuestionAnswer
        public async Task<IActionResult> SubmitAnswerComment([FromBody]Comment comment)
        {
            Answer answer = _questionsService.FindAnswerById(comment.PostId);
            ApplicationUser usr = await GetCurrentUserAsync();
            comment.PublisherId = usr.Id;
            answer.Comments.Add(comment);
            _questionsService.UpdateAnswer(answer);            
            return new ObjectResult(comment);
        }

        //// POST: api/Voter/FlagQuestion
        //public IActionResult FlagQuestion([FromBody]Question question)
        //{
        //    _questionsService.UpdateQuestion(question);
        //    return new ObjectResult(question);
        //}

        //// POST: api/Voter/DeleteQuestion
        //public IActionResult DeleteQuestion([FromBody]Question question)
        //{
        //    _questionsService.UpdateQuestion(question);
        //    return new ObjectResult(question);
        //}

        [HttpPost]
        public IActionResult SaveChanges([FromBody]Question question)
        {
            _dataService.Update(question);
            return new ObjectResult(question);
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {           
            return new ObjectResult(await GetCurrentUserAsync());
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
