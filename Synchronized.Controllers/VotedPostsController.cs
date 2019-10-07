using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Synchronized.Core.Interfaces;
using Synchronized.ServiceModel;
using Synchronized.UI.Utilities.Interfaces;
using Synchronized.ViewModel;
using System.Threading.Tasks;

namespace Synchronized.Controllers
{
    /// <summary>
    /// This controller creates a web API for manipulating Voted Posts.
    /// </summary>
    public class VotedPostsController: SynchronizedController
    {
        private IVotedPostService _votedPostsService;
        private IPostsService<Comment> _commentsService;

        public VotedPostsController(
            IPostsService<Comment> commentsService,
            IVotedPostService votedPostsService,
            IPostsConverter converter,
            ILogger<VotedPostsController> logger,
            UserManager<Domain.ApplicationUser> userManager
        ): base(converter, userManager, logger)
        {
            _commentsService = commentsService;
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
        public async Task<IActionResult> CommentOnPost([FromBody] CommentViewModel comment)
        {
            var user = await GetUserAsync();
            var userPoints = user != null ? user.Points : 0;
            var userId = user != null ? user.Id : "";
            var userName = user != null ? user.UserName : "";
            var serviceComment = await _votedPostsService.CommentOnPost(comment.VotedPostId, comment.Body, userId, userName, userPoints);
            if (serviceComment == null)
            {
                return BadRequest();
            }
            var viewComment = _dataConverter.Convert(serviceComment);

            return new ObjectResult(viewComment);
        }

        // Post: /api/VotedPosts/AddComment
        public async Task<IActionResult> DeleteComment([FromBody] CommentViewModel comment)
        {
            var user = await GetUserAsync();
            var userId = user != null ? user.Id : "";
            var deleted = await _commentsService.DeleteComment(comment.Id, comment.PublisherId, comment.VotedPostPublisherId, userId);
            if (!deleted)
            {
                return new BadRequestResult();
            }
            return Ok();
        }
    }
}
