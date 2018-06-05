using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Synchronized.Core.Interfaces;
using Synchronized.UI.Utilities.Interfaces;
using Synchronized.ViewModel;
using System.Threading.Tasks;

namespace Synchronized.Controllers
{
    public class VotedPostsController: SynchronizedController
    {
        private IVotedPostService _votedPostsService;
        private readonly ILogger<VotedPostsController> _logger;

        public VotedPostsController(
            IVotedPostService votedPostsService,
            IDataConverter converter,
            ILogger<VotedPostsController> logger,
            UserManager<Domain.ApplicationUser> userManager
        ): base(converter, userManager)
        {
            _votedPostsService = votedPostsService;
            _logger = logger;
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

        // POST: /api/VotedPosts/DeletePost
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

        // Post: /api/VotedPosts/AddComment
        public async Task<IActionResult> CommentOnPost([FromBody] CommentViewModel viewModelComment)
        {
            var user = await GetUserAsync();
            var userId = user.Id;
            var userName = user.UserName;
            //string userId = await GetUserIdAsync();
            var comment = await _votedPostsService.CommentOnPost(viewModelComment.VotedPostId, viewModelComment.Body, userId, userName);

            return new ObjectResult(comment);
        }
    }
}
