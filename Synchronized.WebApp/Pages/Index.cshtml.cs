using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.Core.Interfaces;
using Synchronized.Model;
using Synchronized.SharedLib.Utilities;
using Synchronized.WebApp.Conventions;
//using Synchronized.WebApp.Conventions;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        public PaginatedList<Question> Questions { get; set; }

        public int CurrentPage { get; set; }

        private const int PAGE_SIZE = 20; 
        private readonly IQuestionService _service;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            IQuestionService service, 
            ILogger<IndexModel> logger
            )
        {
            _service = service;
            _logger = logger;
            CurrentPage = 1;
        }

        public async Task OnGetAsync([MustBeInRouteParameterQueryConvention]int? pageNumber, [MustBeInRouteParameterQueryConvention]string sortOrder = null)
        {
            CurrentPage = pageNumber ?? 1;
            Questions = await _service.GetQuestionsPage(CurrentPage, PAGE_SIZE);
        }
    }
}
