using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synchronized.Core.Interfaces;
using Synchronized.Model;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Questions
{
    public class AskModel : PageModel
    {
        private IQuestionsService _service;

        public AskModel(IQuestionsService service)
        {
            _service = service;
        }

        [BindProperty]
        public Question Question { get; set; }

        [BindProperty]
        public string TagNames { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _service.CreateAsync(Question);
            return RedirectToPage("/Index");
        }
    }
}