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
        private IQuestionsService _questionsService;
        private IVotedPostService _votedPostsService;
        private readonly IPostsService<Post> _postsService;
        private readonly IDataConverter _dataConverter;
        private readonly ILogger<PostsController> _logger;
        private readonly UserManager<Domain.ApplicationUser> _userManager;

        public PostsController(
            IQuestionsService questionsService,
            IVotedPostService votedPostsService,
            IPostsService<Post> postsService,
            IDataConverter converter,
            ILogger<PostsController> logger,
            UserManager<Domain.ApplicationUser> userManager
        )
        {
            _questionsService = questionsService;
            _votedPostsService = votedPostsService;
            _postsService = postsService;
            _dataConverter = converter;            
            _logger = logger;
            _userManager = userManager;
        }

        // POST: /api/Posts/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteUpQuestion([FromBody]string postId)
        {
            string userId = await GetUserIdAsync();
            var question = await _questionsService.VoteForQuestion(postId, VoteType.UpVote, userId);            
            var questionView = ((IQuestionsConverter)_dataConverter).Convert(question);
            return new ObjectResult(questionView);
        }

        // POST: /api/Posts/VoteDownQuestion
        [HttpPost]
        public async Task<IActionResult> VoteDownQuestion([FromBody]string postId)
        {
            string userId = await GetUserIdAsync();
            var question = await _questionsService.VoteForQuestion(postId, VoteType.DownVote, userId);
            var questionView = ((IQuestionsConverter)_dataConverter).Convert(question);
            return new ObjectResult(questionView);
        }

        // POST: /api/Posts/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteUpAnswer([FromBody]string postId)
        {
            string userId = await GetUserIdAsync();
            var answer = await _questionsService.VoteForAnswer(postId, VoteType.UpVote, userId);
            var answerView = _dataConverter.Convert(answer);
            return new ObjectResult(answerView);
        }

        // POST: /api/Posts/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteDownAnswer([FromBody]string postId)
        {
            string userId = await GetUserIdAsync();
            var answer = await _questionsService.VoteForAnswer(postId, VoteType.DownVote, userId);
            var answerView = _dataConverter.Convert(answer);
            return new ObjectResult(answerView);
        }

        // POST: /api/Posts/FlagPost
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> FlagPost([FromBody]string postId)
        {
            string userId = await GetUserIdAsync();
            var flagged = await _votedPostsService.FlagPost(postId, userId);

            if (!flagged)
            {
                return new BadRequestResult();
            }
            return Ok();
        }

        // POST: /api/Posts/DeletePost

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

        private async Task<string> GetUserIdAsync()
        {
            string userId = null;
            var user = await GetCurrentUserAsync();
            userId = user?.Id;
            return userId;
        }

        private Task<Domain.ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
