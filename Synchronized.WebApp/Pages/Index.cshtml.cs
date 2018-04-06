using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.Core.Interfaces;
using Synchronized.Model;
using Synchronized.SharedLib.Utilities;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        public PaginatedList<Question> Questions { get; set; }

        private readonly IQuestionService _service;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            IQuestionService service, 
            ILogger<IndexModel> logger
            )
        {
            _service = service;
            _logger = logger;
        }

        public async Task OnGetAsync(int? page, int? pageNumber)
        {
            ViewData["maxPage"]= ViewData["maxPage"] == null ? 1 : ViewData["maxPage"];
            var currentPage = pageNumber ?? (page ?? 1);
            int pageSize = 20;
            Questions = await _service.GetQuestionsPage(currentPage, pageSize);
        }
    }
}
