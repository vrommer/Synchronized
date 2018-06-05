using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synchronized.Core.Interfaces;
using Synchronized.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Questions
{
    public class EditModel : PageModel
    {
        private IQuestionsServiceOld _questionsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(IQuestionsServiceOld questionsService,
            UserManager<ApplicationUser> userManager)
        {
            _questionsService = questionsService;
            _userManager = userManager;
        }

        [BindProperty]
        public Question Question { get; set; }

        [BindProperty]
        public string TagNames { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            return null;
        }
    }
}