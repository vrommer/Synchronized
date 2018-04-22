using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.Core.Interfaces;
using Synchronized.Model;
using Synchronized.SharedLib.Utilities;
using Synchronized.WebApp.Conventions;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Questions
{
    public class IndexModel : PageModel
    {
        public PaginatedList<Question> Questions { get; set; }

        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
        public string DateSort { get; set; }
        public string AnswersSort { get; set; }
        public string ViewsSort { get; set; }
        public string PointsSort { get; set; }
        public string SortOrder { get; set; }

        private const int PAGE_SIZE = 20;
        private readonly IQuestionsService _service;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            IQuestionsService service,
            ILogger<IndexModel> logger
            )
        {
            _service = service;
            _logger = logger;
        }

        public async Task OnGetAsync([MustBeInQueryParameterConvention]int? pageNumber, [MustBeInQueryParameterConvention]string sortOrder = null,
            [MustBeInQueryParameterConvention]string searchString = null)
        {
            SearchString = searchString ?? "";
            SortOrder = sortOrder ?? "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            AnswersSort = sortOrder == "Answers" ? "answers_desc" : "Answers";
            ViewsSort = sortOrder == "Views" ? "views_desc" : "Views";
            PointsSort = sortOrder == "Points" ? "points_desc" : "Points";

            CurrentPage = pageNumber ?? (CurrentPage == 0 ? 1 : CurrentPage);

            Questions = await _service.GetQuestionsPageWithUsersAsync(CurrentPage, PAGE_SIZE, sortOrder, SearchString);
        }
    }
}