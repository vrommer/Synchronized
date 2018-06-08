using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel.TagsViewModels;
using Synchronized.ViewServices.Interfaces;
using Synchronized.WebApp.Conventions;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Tags
{
    public class IndexModel : PageModel
    {
        public PaginatedList<TagViewModel> Tags { get; set; }

        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }

        private readonly ITagsService _tagsService;
        private readonly ILogger<IndexModel> _logger;


        public IndexModel(
            ITagsService tagsService,
            ILogger<IndexModel> logger
            )
        {
            _tagsService = tagsService;
            _logger = logger;
        }

        public void OnGet([MustBeInQueryParameterConvention]int? pageNumber, [MustBeInQueryParameterConvention]string sortOrder = null)
        {
            CurrentPage = pageNumber ?? 1;
            Tags = _tagsService.GetIndexPage(CurrentPage);
        }
    }
}