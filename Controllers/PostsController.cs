using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedLib.Infrastructure.Constants;
using Synchronized.Core.Interfaces;
using Synchronized.Domain;
using Synchronized.Model;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Controllers
{
    [Produces("application/json")]
    public class PostsController : Controller
    {
        private IQuestionsService _questionsService;
        private IPostsService _postsService;

        private readonly ILogger<PostsController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostsController(
            IQuestionsService questionsService,
            IPostsService postsService,
            ILogger<PostsController> logger,
            UserManager<ApplicationUser> userManager
            )
        {
            _questionsService = questionsService;
            _postsService = postsService;
            _logger = logger;
            _userManager = userManager;
        }

        // POST: /api/Posts/VoteUpPost
        [HttpPost]
        public async Task<IActionResult> VoteUpPost([FromBody]string postId)
        {
            var user = await GetCurrentUserAsync();
            var post = await _postsService.FindtPostOfType<CommentedPost>(p => p.Id.Equals(postId));

            if (user != null  && !post.Votes.Contains(new Vote {
                VoterId = user.Id,
                PostId = postId
            }))
            {
                post.Votes.Add(new Vote
                {
                    Voter = user,
                    Post = post,
                    VoteType = (int)VoteType.UpVote
                });
                _postsService.Update(post);
            }
            return new ObjectResult(post);
        }

        // POST: /api/Questions/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteUpQuestion([FromBody]string questionId)
        {
            var user = await GetCurrentUserAsync();
            var question = _questionsService.FindQuestionById(questionId);
            question.Points++;
            _questionsService.UpdateQuestion(question);
            return new ObjectResult(question);
        }

        // POST: api/Voter/VoteDownQuestion
        [HttpPost]
        public async Task<IActionResult> VoteDownQuestion([FromBody]string questionId)
        {
            var user = await GetCurrentUserAsync();
            var question = _questionsService.FindQuestionById(questionId);
            question.Points--;
            _questionsService.UpdateQuestion(question);
            return new ObjectResult(question);
        }

        // POST: api/Voter/VoteUpAnswer
        [HttpPost]
        public async Task<IActionResult> VoteUpAnswer([FromBody]Answer answer)
        {
            var user = await GetCurrentUserAsync();
            answer.Points++;
            _questionsService.UpdateAnswer(answer);
            return new ObjectResult(answer);
        }

        // POST: api/Voter/VoteDownAnswer
        [HttpPost]
        public async Task<IActionResult> VoteDownAnswer([FromBody]Answer answer)
        {
            var user = await GetCurrentUserAsync();
            answer.Points--;
            _questionsService.UpdateAnswer(answer);
            return new ObjectResult(answer);
        }

        // POST: api/Voter/SubmitQuestionComment
        [HttpPost]
        public async Task<IActionResult> SubmitQuestionComment([FromBody]Comment comment)
        {
            var user = await GetCurrentUserAsync();
            var question = _questionsService.FindQuestionById(comment.PostId);
            if (user != null)
            {
                comment.PublisherId = user.Id;
                question.Comments.Add(comment);
                _questionsService.UpdateQuestion(question);
                return new ObjectResult(comment);
            }
            return null;
        }

        // POST: api/Voter/SubmitQuestionAnswer
        public async Task<IActionResult> SubmitAnswerComment([FromBody]Comment comment)
        {
            var user = await GetCurrentUserAsync();
            var answer = _questionsService.FindAnswerById(comment.PostId);
            if (user != null)
            {
                comment.PublisherId = user.Id;
                answer.Comments.Add(comment);
                _questionsService.UpdateAnswer(answer);
                return new ObjectResult(comment);
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost([FromBody]string postId)
        {
            var user = await GetCurrentUserAsync();
            var post = _postsService.FindPostById(postId);
            if (user != null && !post.DeleteVotes.Contains(new DeleteVote
            {
                UserId = user.Id,
                PostId = post.Id
            }))
            {
                // EF Core will not track new entity with key
                post.DeleteVotes.Add(new DeleteVote
                {
                    Post = post,
                    User = user
                });
                _postsService.Update(post);
            }
            return new ObjectResult(post);
        }

        [HttpPost]
        public async Task<IActionResult> FlagPost([FromBody]string postId)
        {
            var user = await GetCurrentUserAsync();
            var post = _postsService.FindPostById(postId);
            if (user != null && !post.PostFlags.Contains(new PostFlag
            {
                UserId = user.Id,
                PostId = post.Id
            }))
            {
                // EF Core will not track new entity with key
                post.PostFlags.Add(new PostFlag
                {
                    Post = post,
                    User = user
                });
                _postsService.Update(post);
            }
            return new ObjectResult(post);
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
