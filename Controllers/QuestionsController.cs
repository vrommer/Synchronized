using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedLib.Infrastructure.Constants;
using Synchronized.Core.Interfaces;
using Synchronized.UI.Utilities.Interfaces;
using Synchronized.ViewModel;
using System.Threading.Tasks;

namespace Synchronized.Controllers
{
    public class QuestionsController: SynchronizedController
    {
        private IQuestionsService _questionsService;
        private IUsersService _usersService;

        public QuestionsController(
            IUsersService usersService,
            IQuestionsService questionsService,
            IPostsConverter converter,
            ILogger<QuestionsController> logger,
            UserManager<Domain.ApplicationUser> userManager
        ) : base(converter, userManager, logger)
        {
            _usersService = usersService;
            _questionsService = questionsService;
        }

        // POST: /api/Questions/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteUpQuestion([FromBody]string postId)
        {
            var user = await GetUserAsync();
            var question = await _questionsService.VoteForQuestion(postId, VoteType.UpVote, user);
            if (question == null)
            {
                return BadRequest();
            }
            await _usersService.UpdateUserRoles(question.PublisherId);
            var questionView = ((IQuestionsConverter)_dataConverter).Convert(question);
            return new ObjectResult(questionView);
        }

        // POST: /api/Questions/VoteDownQuestion
        [HttpPost]
        public async Task<IActionResult> VoteDownQuestion([FromBody]string postId)
        {
            var user = await GetUserAsync();
            var question = await _questionsService.VoteForQuestion(postId, VoteType.DownVote, user);
            if (question == null)
            {
                return BadRequest();
            }
            await _usersService.UpdateUserRoles(question.PublisherId);
            await _usersService.UpdateUserRoles(user);
            var questionView = ((IQuestionsConverter)_dataConverter).Convert(question);
            return new ObjectResult(questionView);
        }

        // POST: /api/Questions/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteUpAnswer([FromBody]string postId)
        {
            var user = await GetUserAsync();           
            var answer = await _questionsService.VoteForAnswer(postId, VoteType.UpVote, user);
            if (answer == null)
            {
                return BadRequest();
            }
            await _usersService.UpdateUserRoles(answer.PublisherId);
            var answerView = _dataConverter.Convert(answer);
            return new ObjectResult(answerView);
        }

        // POST: /api/Questions/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteDownAnswer([FromBody]string postId)
        {
            var user = await GetUserAsync();
            var userPoints = user != null ? user.Points : 0;
            var userId = user != null ? user.Id : "";
            var answer = await _questionsService.VoteForAnswer(postId, VoteType.DownVote, user);
            if (answer == null)
            {
                return BadRequest();
            }
            await _usersService.UpdateUserRoles(answer.PublisherId);
            await _usersService.UpdateUserRoles(user);
            var answerView = _dataConverter.Convert(answer);
            return new ObjectResult(answerView);
        }

        // POST: /api/Questions/AcceptAnswer
        [HttpPost]
        public async Task<IActionResult> AcceptAnswer([FromBody]AnswerViewModel answer)
        {
            var user = await GetUserAsync();
            string userId = user.Id;
            await _questionsService.AcceptAnswer(answer, user);
            await _usersService.UpdateUserRoles(answer.PublisherId);
            await _usersService.UpdateUserRoles(user);
            return new ObjectResult(answer);
        }
    }
}
