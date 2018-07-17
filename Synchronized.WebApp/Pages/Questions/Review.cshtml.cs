using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel.QuestionsViewModels;
using Synchronized.ViewServices.Interfaces;
using Synchronized.WebApp.Conventions;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Questions
{
    public class ReviewModel : PageModel
    {
        public PaginatedList<QuestionForQuestionsPage> Questions { get; set; }

        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
        public string DateSort { get; set; }
        public string AnswersSort { get; set; }
        public string ViewsSort { get; set; }
        public string PointsSort { get; set; }
        public string SortOrder { get; set; }

        private const int PAGE_SIZE = 20;
        //private readonly IQuestionsService _service;
        private readonly IQuestionsService _localService;
        private readonly ILogger<ReviewModel> _logger;

        public ReviewModel(
            //IQuestionsService service,
            IQuestionsService localService,
            ILogger<ReviewModel> logger
            )
        {
            //_service = service;
            _localService = localService;
            _logger = logger;
        }

        public async Task OnGetAsync([MustBeInQueryParameterConvention]int? pageNumber)
        {
            _logger.LogInformation("Starting OnGetAsync");

            CurrentPage = pageNumber ?? (CurrentPage == 0 ? 1 : CurrentPage);

            Questions = await _localService.GetPageForReview(CurrentPage);
            _logger.LogInformation("Exiting OnGetAsync");
        }
    }
}