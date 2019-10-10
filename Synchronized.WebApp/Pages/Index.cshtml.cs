using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel.QuestionsViewModels;
using Synchronized.ViewServices.Interfaces;
using Synchronized.WebApp.Conventions;
using System.Net.Http;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        public PaginatedList<QuestionForHomePage> Questions { get; set; }

        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private readonly IQuestionsService _questionsService;        

        public IndexModel(
            ILogger<IndexModel> logger,
            IQuestionsService localService
            )
        {
            _questionsService = localService;
            _logger = logger;
            CurrentPage = 1;
        }

        public async Task OnGetAsync([MustBeInQueryParameterConvention]int? pageNumber, [MustBeInQueryParameterConvention]string sortOrder = null)
        {                                 
            CurrentPage = pageNumber ?? 1;
            Questions = await _questionsService.GetHomePageModel(CurrentPage);
        }
    }
}
