using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedLib.Infrastructure.Constants;
using Synchronized.Core.Interfaces;
using Synchronized.SharedLib;
using Synchronized.UI.Utilities.Interfaces;
using Synchronized.ViewModel;
using System.Threading.Tasks;

namespace Synchronized.Controllers
{
    public class QuestionsController: SynchronizedController
    {
        private IQuestionsService _questionsService;

        public QuestionsController(
            IQuestionsService questionsService,
            IPostsConverter converter,
            ILogger<QuestionsController> logger,
            UserManager<Domain.ApplicationUser> userManager
        ) : base(converter, userManager, logger)
        {
            _questionsService = questionsService;
        }

        // POST: /api/Questions/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteUpQuestion([FromBody]string postId)
        {
            var user = await GetUserAsync();
            var userPoints = user != null ? user.Points : 0;
            var userId = user != null ? user.Id : "";
            var question = await _questionsService.VoteForQuestion(postId, VoteType.UpVote, userId, userPoints);
            if (question == null)
            {
                return BadRequest();
            }
            var questionView = ((IQuestionsConverter)_dataConverter).Convert(question);
            return new ObjectResult(questionView);
        }

        // POST: /api/Questions/VoteDownQuestion
        [HttpPost]
        public async Task<IActionResult> VoteDownQuestion([FromBody]string postId)
        {
            var user = await GetUserAsync();
            var userPoints = user != null ? user.Points : 0;
            var userId = user != null ? user.Id : "";
            var question = await _questionsService.VoteForQuestion(postId, VoteType.DownVote, userId, userPoints);
            if (question == null)
            {
                return BadRequest();
            }
            var questionView = ((IQuestionsConverter)_dataConverter).Convert(question);
            return new ObjectResult(questionView);
        }

        // POST: /api/Questions/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteUpAnswer([FromBody]string postId)
        {
            var user = await GetUserAsync();
            var userPoints = user != null ? user.Points : 0;
            var userId = user != null ? user.Id : "";           
            var answer = await _questionsService.VoteForAnswer(postId, VoteType.UpVote, userId, userPoints);
            if (answer == null)
            {
                return BadRequest();
            }
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
            var answer = await _questionsService.VoteForAnswer(postId, VoteType.DownVote, userId, userPoints);
            if (answer == null)
            {
                return BadRequest();
            }
            var answerView = _dataConverter.Convert(answer);
            return new ObjectResult(answerView);
        }

        // POST: /api/Questions/AcceptAnswer
        [HttpPost]
        public async Task<IActionResult> AcceptAnswer([FromBody]AnswerViewModel answer)
        {
            string userId = await GetUserIdAsync();
            await _questionsService.AcceptAnswer(answer, userId);
            return new ObjectResult(answer);
        }
    }
}
