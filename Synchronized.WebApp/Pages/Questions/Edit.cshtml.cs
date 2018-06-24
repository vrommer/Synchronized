using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Synchronized.Domain;
using Synchronized.UI.Utilities;
using Synchronized.ViewModel;
using Synchronized.ViewServices.Interfaces;
using System;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Pages.Questions
{
    public class EditModel : PageModel
    {
        private IPostsService _service;
        private ILogger<DetailsModel> _logger;

        public EditModel(
            IPostsService service,
            ILogger<DetailsModel> logger
        )
        {
            _service = service;
            _logger = logger;
        }
        [BindProperty]
        public EditViewModel Post { get; set; }

        public async Task OnGetAsync(string id)
        {
            Post = await _service.GetPostForEdit(id);
        }

        public async Task<IActionResult> OnPostAsync(string id, string questionId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Post.Id = String.Copy(id);
            Post.QuestionId = String.Copy(questionId);
            var postId = await _service.UpdatePost(Post);

            return RedirectToPage("/Questions/Details", new { id = postId });
        }
    }
}