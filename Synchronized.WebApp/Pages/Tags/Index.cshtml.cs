using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.Core.Interfaces;
using Synchronized.Domain;
using Synchronized.SharedLib.Utilities;
using Synchronized.WebApp.Conventions;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Tags
{
    public class IndexModel : PageModel
    {
        public PaginatedList<Tag> Tags { get; set; }

        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }

        private const int PAGE_SIZE = 20;
        private readonly ITagsService _service;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            ITagsService service,
            ILogger<IndexModel> logger
        )
        {
            _service = service;
            _logger = logger;
            CurrentPage = 1;
        }

        public async Task OnGetAsync([MustBeInQueryParameterConvention]int? pageNumber, [MustBeInQueryParameterConvention]string searchString = null)
        {
            CurrentPage = pageNumber ?? 1;
            Tags = await _service.GetTagsPageAsync(CurrentPage, PAGE_SIZE);
        }
    }
}