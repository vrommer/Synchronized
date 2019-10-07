using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel.QuestionsViewModels;
using Synchronized.ViewServices.Interfaces;
using Synchronized.WebApp.Conventions;
using System.Net.Http;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        public PaginatedList<QuestionForHomePage> Questions { get; set; }

        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }
        public HttpClient Client { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private readonly IQuestionsService _questionsService;        

        public IndexModel(
            ILogger<IndexModel> logger,
            IQuestionsService localService,
            HttpClient client
            )
        {
            _questionsService = localService;
            _logger = logger;
            Client = client;
            CurrentPage = 1;
        }

        public async Task OnGetAsync([MustBeInQueryParameterConvention]int? pageNumber, [MustBeInQueryParameterConvention]string sortOrder = null)
        {         
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, @"http://174.138.105.248/notifications");
            httpRequest.Content = new StringContent("{" +
                "\"recepients\":[\"vadim.rommer@gmail.com\"]," +
                "\"subject\":\"Synchronized home page\"," +
                "\"message\":\"Someone viewd synchronized homepage\"}",
                                                Encoding.UTF8,
                                                "application/json");

            try {
                var result = await Client.SendAsync(httpRequest);
            } catch (Exception ex)
            {
                _logger.LogDebug("Notification not sent!");
                _logger.LogDebug(ex.Message);
            }
            
            CurrentPage = pageNumber ?? 1;
            Questions = await _questionsService.GetHomePageModel(CurrentPage);
        }
    }
}
