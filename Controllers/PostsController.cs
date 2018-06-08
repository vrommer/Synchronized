using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedLib.Infrastructure.Constants;
using Synchronized.Controllers;
using Synchronized.Core.Interfaces;
using Synchronized.ServiceModel;
using Synchronized.UI.Utilities.Interfaces;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Controllers
{
    [Produces("application/json")]
    public class PostsController : SynchronizedController
    {
        private IQuestionsService _questionsService;
        private IVotedPostService _votedPostsService;
        private readonly IPostsService<Post> _postsService;
        private readonly ILogger<PostsController> _logger;

        public PostsController(
            IQuestionsService questionsService,
            IVotedPostService votedPostsService,
            IPostsService<Post> postsService,
            IPostsConverter converter,
            ILogger<PostsController> logger,
            UserManager<Domain.ApplicationUser> userManager
        ): base(converter, userManager)
        {
            _questionsService = questionsService;
            _votedPostsService = votedPostsService;
            _postsService = postsService;     
            _logger = logger;
        }

        // POST: /api/Posts/DeletePost
        public async Task<IActionResult> DeletePost([FromBody] string postId)
        {
            string userId = await GetUserIdAsync();
            var deleted = await _votedPostsService.DeletePost(postId, userId);

            if (!deleted)
            {
                return new BadRequestResult();
            }
            return Ok();
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
    }
}
