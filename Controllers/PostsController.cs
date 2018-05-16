using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SharedLib.Infrastructure.Constants;
using Synchronized.Core.Interfaces;
using Synchronized.ServiceModel;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Controllers
{
    [Produces("application/json")]
    public class PostsController : Controller
    {
        private IQuestionsService _questionsService;
        private IPostsService _postsService;

        private readonly ILogger<PostsController> _logger;
        private readonly UserManager<Model.ApplicationUser> _userManager;

        public PostsController(
            IQuestionsService questionsService,
            IPostsService postsService,
            ILogger<PostsController> logger,
            UserManager<Model.ApplicationUser> userManager
        )
        {
            _questionsService = questionsService;
            _postsService = postsService;
            _logger = logger;
            _userManager = userManager;
        }

        // POST: /api/Posts/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteUpQuestion([FromBody]Question post)
        {
            _logger.LogTrace("Entering VoteUpQuestion method");
            await VoteForPost(post, VoteType.UpVote);
            return new ObjectResult(post);
        }

        // POST: /api/Posts/VoteDownQuestion
        [HttpPost]
        public async Task<IActionResult> VoteDownQuestion([FromBody]Question post)
        {
            _logger.LogTrace("Entering VoteDownQuestion method");
            await VoteForPost(post, VoteType.DownVote);
            return new ObjectResult(post);
        }

        // POST: /api/Posts/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteUpAnswer([FromBody]Answer post)
        {
            _logger.LogTrace("Entering VoteUpAnswer method");
            await VoteForPost(post, VoteType.UpVote);
            return new ObjectResult(post);
        }

        // POST: /api/Posts/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteDownAnswer([FromBody]Answer post)
        {
            _logger.LogTrace("Entering VoteDownAnswer method");
            await VoteForPost(post, VoteType.DownVote);
            return new ObjectResult(post);
        }

        // POST: api/Posts/AddComment
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody]Comment comment)
        {
            var user = await GetCurrentUserAsync();
            var result = _postsService.CommentOnPost(user, comment);
            return new ObjectResult(result);
        }

        // POST: api/Posts/Delete
        //public async Task<IActionResult> Delete([FromBody]Post post)
        //{
        //    _logger.LogDebug($"{post.GetType().FullName}");
        //    return Ok(new { id = post.Id });
        //}


        // POST: api/Posts/Flag

        /**********************************************************************/

        // POST: /api/Posts/VoteUpPost
        //[HttpPost]
        //public async Task<IActionResult> VoteUpPost([FromBody]string postId)
        //{
        //    var user = await GetCurrentUserAsync();
        //    var post = await _postsService.FindPostOfType<VotedPost>(p => p.Id.Equals(postId));
        //    var vote = new Vote
        //    {
        //        VoterId = user.Id,
        //        PostId = postId,
        //        VoteType = (int)VoteType.UpVote                
        //    };

        //    if (user != null  && !post.Votes.Contains(vote))
        //    {
        //        //post.Votes.Add(vote);
        //        _postsService.Add(post);
        //        return new ObjectResult(vote);
        //    }
        //    return null;
        //}

        // POST: api/Voter/SubmitQuestionComment
        [HttpPost]
        public async Task<IActionResult> SubmitQuestionComment([FromBody]Model.Comment comment)
        {
            var user = await GetCurrentUserAsync();
            var question = _questionsService.FindQuestionById(comment.PostId);
            if (user != null)
            {
                comment.PublisherId = user.Id;
                //question.Comments.Add(comment);
                _questionsService.UpdateQuestion(question);
                return new ObjectResult(comment);
            }
            return null;
        }

        // POST: api/Voter/SubmitQuestionAnswer
        public async Task<IActionResult> SubmitAnswerComment([FromBody]Model.Comment comment)
        {
            var user = await GetCurrentUserAsync();
            var answer = _questionsService.FindAnswerById(comment.PostId);
            if (user != null)
            {
                comment.PublisherId = user.Id;
                //answer.Comments.Add(comment);
                _questionsService.UpdateAnswer(answer);
                return new ObjectResult(comment);
            }
            return null;
        }

        //[HttpPost]
        //public async Task<IActionResult> DeletePost([FromBody]string postId)
        //{
        //    var user = await GetCurrentUserAsync();
        //    var post = _postsService.FindPostById(postId);
        //    if (user != null && !post.DeleteVotes.Contains(new DeleteVote
        //    {
        //        UserId = user.Id,
        //        PostId = post.Id
        //    }))
        //    {
        //        // EF Core will not track new entity with key
        //        post.DeleteVotes.Add(new DeleteVote
        //        {
        //            Post = post,
        //            User = user
        //        });
        //        _postsService.Update(post);
        //    }
        //    return new ObjectResult(post);
        //}

        //[HttpPost]
        //public async Task<IActionResult> FlagPost([FromBody]string postId)
        //{
        //    var user = await GetCurrentUserAsync();
        //    var post = _postsService.FindPostById(postId);
        //    if (user != null && !post.PostFlags.Contains(new PostFlag
        //    {
        //        UserId = user.Id,
        //        PostId = post.Id
        //    }))
        //    {
        //        // EF Core will not track new entity with key
        //        post.PostFlags.Add(new PostFlag
        //        {
        //            Post = post,
        //            User = user
        //        });
        //        _postsService.Update(post);
        //    }
        //    return new ObjectResult(post);
        //}

        private async Task VoteForPost(VotedPost post, VoteType voteType) {
            var user = await GetCurrentUserAsync();
            await _postsService.VoteForPost(post, user, voteType);
        }

        private Task<Model.ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
