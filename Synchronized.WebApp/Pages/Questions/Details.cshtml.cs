using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.UI.Utilities;
using Synchronized.ViewModel;
using Synchronized.ViewModel.QuestionsViewModels;
using Synchronized.ViewServices.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Questions
{
    public class DetailsModel : PageModel
    {
        public QuestionForDetailsPage Question { get; set; }
        public string UserId { get; set; }
        public HttpClient Client { get; set; }

        private readonly ILogger<DetailsModel> _logger;
        IQuestionsService _questionsService;
        private readonly UserManager<Domain.ApplicationUser> _userManager;

        public DetailsModel(
            IQuestionsService questionsService,
            ILogger<DetailsModel> logger,
            UserManager<Domain.ApplicationUser> userManager,
            HttpClient client
        )
        {
            _questionsService = questionsService;
            _logger = logger;
            _userManager = userManager;
            Client = client;
        }

        public async Task OnGetAsync(string id)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, @"http://174.138.105.248/notifications");
            httpRequest.Content = new StringContent("{" +
                "\"recepients\":[\"vadim.rommer@gmail.com\"]," +
                "\"subject\":\"Synchronized home page\"," +
                "\"message\":\"Someone viewd synchronized details page for post: " + id + "\"}",
                                                Encoding.UTF8,
                                                "application/json");

            try
            {
                var result = await Client.SendAsync(httpRequest);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Notification not sent!");
                _logger.LogDebug(ex.Message);
            }
            UserId = await Utils.GetUserIdAsync(HttpContext, _userManager);
            Question = await _questionsService.GetQuestionDetailsPageModel(id, UserId);
        }

        [BindProperty]
        public AnswerViewModel Answer { get; set; }

        public async Task<IActionResult> OnPostAsync(string id)
        {            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userId = await Utils.GetUserIdAsync(HttpContext, _userManager);
            await _questionsService.AnswerQuestion(Answer, userId, id);

            return RedirectToPage("/Questions/Details", new { id = id });
        }
    }
}