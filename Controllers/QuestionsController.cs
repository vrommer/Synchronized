using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Synchronized.Core.Interfaces;
using Synchronized.Domain;
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
        public IActionResult VoteUpQuestion([FromBody]string questionId)
        {
            var question = _questionsService.FindQuestionById(questionId);
            question.Points++;
            _questionsService.UpdateQuestion(question);
            return new ObjectResult(question);
        }

        // POST: api/Voter/VoteDownQuestion
        [HttpPost]
        public IActionResult VoteDownQuestion([FromBody]string questionId)
        {
            var question = _questionsService.FindQuestionById(questionId);
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
            var question = _questionsService.FindQuestionById(comment.PostId);
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

        [HttpPost]
        public async Task<IActionResult> DeleteQuestion([FromBody]string questionId)
        {
            var question = _questionsService.FindQuestionById(questionId);
            var user = await GetCurrentUserAsync();
            if (!question.DeleteVotes.Contains(new DeleteVote
            {
                UserId = user.Id,
                QuestionId = question.Id
            }))
            {
                // EF Core will not track new entity with key
                question.DeleteVotes.Add(new DeleteVote
                {
                    Question = question,
                    User = user
                });
                _questionsService.UpdateQuestion(question);
            }
            return new ObjectResult(question);
        }

        [HttpPost]
        public async Task<IActionResult> FlagQuestion([FromBody]string questionId)
        {
            var question = _questionsService.FindQuestionById(questionId);
            var user = await GetCurrentUserAsync();
            if (!question.QuestionFlags.Contains(new QuestionFlag
            {
                UserId = user.Id,
                QuestionId = question.Id
            }))
            {
                // EF Core will not track new entity with key
                question.QuestionFlags.Add(new QuestionFlag
                {
                    Question = question,
                    User = user
                });
                _questionsService.UpdateQuestion(question);
            }
            return new ObjectResult(question);
        }

        public async Task<IActionResult> GetCurrentUser()
        {           
            return new ObjectResult(await GetCurrentUserAsync());
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
