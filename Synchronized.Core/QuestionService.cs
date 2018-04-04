using Synchronized.Core.Interfaces;
using Synchronized.Model;
using Synchronized.Repository.Interfaces;
using UtilsLib.HtmlUtils.HtmlParser;
using Synchronized.ViewModel;
using Synchronized.Core.Utilites;
using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;

namespace Synchronized.Core
{
    public class QuestionService : DataService<Question>, IQuestionService
    {
        private readonly IQuestionsRepository questionsRepo;
        private readonly HtmlParser parser;

        public QuestionService(IQuestionsRepository repo, HtmlParser parser) : base(repo)
        {
            questionsRepo = repo;
            this.parser = parser;
        }

        public async Task<HomeViewModel> GetHomeViewModel(int pageIndex, int pageSize)
        {
            var questions = await questionsRepo.GetQuestionsPageWithTagsAsync(pageIndex, pageSize);
            int count = await repo.GetCount();
            Utils.MinimizeContent(parser, questions);
            var paginatedList = new PaginatedList<Question>(questions, count, pageIndex, pageSize);
            var homeViewModel = new HomeViewModel
            {
                Questions = paginatedList
            };
            return homeViewModel;
        }
    }
}
