using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Synchronized.Controllers;
using Synchronized.Core.Interfaces;
using Synchronized.ServiceModel;
using Synchronized.UI.Utilities.Interfaces;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Controllers
{
    [Produces("application/json")]
    public class CommentsController : SynchronizedController
    {
        private readonly IPostsService<Comment> _postsService;

        public CommentsController(
            IQuestionsService questionsService,
            IVotedPostService votedPostsService,
            IPostsService<Comment> postsService,
            IPostsConverter converter,
            ILogger<CommentsController> logger,
            UserManager<Domain.ApplicationUser> userManager
        ): base(converter, userManager, logger)
        {
            _postsService = postsService;     
        }

        // POST: /api/Posts/DeletePost
        public async Task<IActionResult> DeletePost([FromBody]string postId)
        {
            var user = await GetUserAsync();
            var userPoints = user != null ? user.Points: 0;
            var userId = user != null ? user.Id: "";
            var deleted = await _postsService.DeletePost(postId, userId, userPoints);

            if (!deleted)
            {
                return new BadRequestResult();
            }
            return Ok();
        }
    }
}
