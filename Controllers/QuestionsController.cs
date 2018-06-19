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
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(
            IQuestionsService questionsService,
            IPostsConverter converter,
            ILogger<QuestionsController> logger,
            UserManager<Domain.ApplicationUser> userManager
        ) : base(converter, userManager)
        {
            _questionsService = questionsService;
            _logger = logger;
        }

        // POST: /api/Questions/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteUpQuestion([FromBody]string postId)
        {
            string userId = await GetUserIdAsync();
            var question = await _questionsService.VoteForQuestion(postId, VoteType.UpVote, userId);
            var questionView = ((IQuestionsConverter)_dataConverter).Convert(question);
            return new ObjectResult(questionView);
        }

        // POST: /api/Questions/VoteDownQuestion
        [HttpPost]
        public async Task<IActionResult> VoteDownQuestion([FromBody]string postId)
        {
            string userId = await GetUserIdAsync();
            var question = await _questionsService.VoteForQuestion(postId, VoteType.DownVote, userId);
            var questionView = ((IQuestionsConverter)_dataConverter).Convert(question);
            return new ObjectResult(questionView);
        }

        // POST: /api/Questions/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteUpAnswer([FromBody]string postId)
        {
            string userId = await GetUserIdAsync();
            var answer = await _questionsService.VoteForAnswer(postId, VoteType.UpVote, userId);
            var answerView = _dataConverter.Convert(answer);
            return new ObjectResult(answerView);
        }

        // POST: /api/Questions/VoteUpQuestion
        [HttpPost]
        public async Task<IActionResult> VoteDownAnswer([FromBody]string postId)
        {
            string userId = await GetUserIdAsync();
            var answer = await _questionsService.VoteForAnswer(postId, VoteType.DownVote, userId);
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
