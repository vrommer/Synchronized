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

        public VotedPostsController(
            IVotedPostService votedPostsService,
            IPostsConverter converter,
            ILogger<VotedPostsController> logger,
            UserManager<Domain.ApplicationUser> userManager
        ): base(converter, userManager, logger)
        {
            _votedPostsService = votedPostsService;
        }

        // POST: /api/Posts/FlagPost
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> FlagPost([FromBody]string postId)
        {
            var user = await GetUserAsync();
            var userId = user.Id;
            var userPoints = user.Points;
            var flagged = await _votedPostsService.FlagPost(postId, userId, userPoints);

            if (!flagged)
            {
                return new BadRequestResult();
            }
            return Ok();
        }

        // POST: /api/VotedPosts/DeletePost
        public async Task<IActionResult> DeletePost([FromBody] string postId)
        {
            var user = await GetUserAsync();
            var userPoints = user != null ? user.Points : 0;
            var userId = user != null ? user.Id : "";
            var deleted = await _votedPostsService.DeletePost(postId, userId, userPoints);

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
            var userPoints = user != null ? user.Points : 0;
            var userId = user != null ? user.Id : "";
            var userName = user != null ? user.UserName : "";
            var comment = await _votedPostsService.CommentOnPost(viewModelComment.VotedPostId, viewModelComment.Body, userId, userName, userPoints);
            if (comment == null)
            {
                return BadRequest();
            }
            var viewComment = _dataConverter.Convert(comment);

            return new ObjectResult(viewComment);
        }
    }
}
