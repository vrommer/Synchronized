using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Synchronized.WebApp.Pages.Tags
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public DetailsModel(
            ILogger<IndexModel> logger
            )
        {
            _logger = logger;
        }
    }
}