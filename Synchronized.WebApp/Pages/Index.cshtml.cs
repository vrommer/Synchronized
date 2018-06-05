using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel;
using Synchronized.ViewServices.Interfaces;
using Synchronized.WebApp.Conventions;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        //public PaginatedList<ServiceModel.Question> Questions { get; set; }
        public PaginatedList<QuestionForHomePage> Questions { get; set; }

        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }

        //private readonly IQuestionsService _service;
        private readonly IQuestionsService _localService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            //IQuestionsService service, 
            ILogger<IndexModel> logger,
            IQuestionsService localService
            )
        {
            //_service = service;
            _localService = localService;
            _logger = logger;
            CurrentPage = 1;
        }

        public async Task OnGetAsync([MustBeInQueryParameterConvention]int? pageNumber, [MustBeInQueryParameterConvention]string sortOrder = null)
        {
            CurrentPage = pageNumber ?? 1;
            Questions = await _localService.GetHomePageModel(CurrentPage);
        }
    }
}
