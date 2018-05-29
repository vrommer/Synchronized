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
        private ITagsService _tagsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(IQuestionsServiceOld questionsService,
            ITagsService tagsService,
            UserManager<ApplicationUser> userManager)
        {
            _questionsService = questionsService;
            _tagsService = tagsService;
            _userManager = userManager;
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
            ApplicationUser usr = await GetCurrentUserAsync();

            Question.QuestionTags = new List<QuestionTag>();
            Question.PublisherId = usr.Id;

            string[] TageNamesArray = TagNames.Split(',');
            for (int i = 0; i < TageNamesArray.Length; i++)
            {
                Tag tag = await _tagsService.FindTagByName(TageNamesArray[i]);
                Question.QuestionTags.Add(new QuestionTag
                {
                    TagId = tag.Id
                });
            }

            await _questionsService.CreateAsync(Question);
            return RedirectToPage("/Index");
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}