using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.Core.Interfaces;
using Synchronized.Model;

namespace Synchronized.WebApp.Pages.Questions
{
    public class DetailsModel : PageModel
    {
        public Question Question { get; set; }

        private readonly IQuestionsService _service;
        private readonly ILogger<IndexModel> _logger;

        public DetailsModel(
            IQuestionsService service,
            ILogger<IndexModel> logger
            )
        {
            _service = service;
            _logger = logger;
        }

        public void OnGet(string id)
        {
            Question = _service.FindQuestionById(id);
        }

        [BindProperty]
        public Answer Answer { get; set; }

        public IActionResult OnPost(int? id, int? points)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_service.Update(Question);
            return RedirectToPage("/Index");
        }
    }
}