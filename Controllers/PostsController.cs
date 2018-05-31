using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedLib.Infrastructure.Constants;
using Synchronized.Core.Interfaces;
using Synchronized.ServiceModel;
using Synchronized.UI.Utilities.Interfaces;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Controllers
{
    [Produces("application/json")]
    public class PostsController : Controller
    {
        private IQuestionsService _service;
        private readonly IDataConverter _dataConverter;
        private readonly ILogger<PostsController> _logger;
        private readonly UserManager<Domain.ApplicationUser> _userManager;

        public PostsController(
            IQuestionsService service,
            IDataConverter converter,
            ILogger<PostsController> logger,
            UserManager<Domain.ApplicationUser> userManager
        )
        {
            _service = service;
            _dataConverter = converter;            
            _logger = logger;
            _userManager = userManager;
        }

        // POST: /api/Posts/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteUpQuestion([FromBody]string postId)
        {
            var user = await GetCurrentUserAsync();
            var question = await _service.VoteForQuestion(postId, VoteType.UpVote, user.Id);            
            var questionView = ((IQuestionsConverter)_dataConverter).Convert(question);
            return new ObjectResult(questionView);
        }

        // POST: /api/Posts/VoteDownQuestion
        [HttpPost]
        public async Task<IActionResult> VoteDownQuestion([FromBody]string postId)
        {
            var user = await GetCurrentUserAsync();
            var question = await _service.VoteForQuestion(postId, VoteType.DownVote, user.Id);
            var questionView = ((IQuestionsConverter)_dataConverter).Convert(question);
            return new ObjectResult(questionView);
        }

        // POST: /api/Posts/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteUpAnswer([FromBody]string postId)
        {
            var user = await GetCurrentUserAsync();
            var answer = await _service.VoteForAnswer(postId, VoteType.UpVote, user.Id);
            var answerView = _dataConverter.Convert(answer);
            return new ObjectResult(answerView);
        }

        // POST: /api/Posts/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteDownAnswer([FromBody]string postId)
        {
            var user = await GetCurrentUserAsync();
            var answer = await _service.VoteForAnswer(postId, VoteType.DownVote, user.Id);
            var answerView = _dataConverter.Convert(answer);
            return new ObjectResult(answerView);
        }

        // POST: api/Posts/AddComment
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody]string Body, [FromBody]string postId)
        {
            return null;
        }

        // POST: api/Voter/SubmitQuestionComment
        [HttpPost]
        public async Task<IActionResult> SubmitQuestionComment([FromBody]Domain.Comment comment)
        {
            return null;
        }

        // POST: api/Voter/SubmitQuestionAnswer
        public async Task<IActionResult> SubmitAnswerComment([FromBody]Domain.Comment comment)
        {
            return null;
        }

        private async Task<T> VoteForPost<T>(string postId, VoteType voteType) where T: VotedPost
        {
            var user = await GetCurrentUserAsync();
            var post = await _service.VoteForPost<T>(postId, voteType, user.Id);
            return post;
        }

        private Task<Domain.ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
