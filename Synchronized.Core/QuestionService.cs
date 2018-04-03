using Synchronized.Core.Interfaces;
using Synchronized.Model;
using System;
using Synchronized.Repository.Interfaces;
using UtilsLib.HtmlUtils.HtmlParser;
using Synchronized.ViewModel;
using System.Linq;
using Synchronized.Core.Utilites;
using Synchronized.SharedLib.Utilities;
using System.Threading.Tasks;

namespace Synchronized.Core
{
    public class QuestionService : DataService<Question>, IQuestionService
    {
        private readonly HtmlParser parser;

        public QuestionService(IDataRepository<Question> repo, HtmlParser parser) : base(repo)
        {
            this.parser = parser;
        }

        public async Task<HomeViewModel> GetHomeViewModel(int pageIndex, int pageSize)
        {
            var questions = repo.GetAll().AsQueryable();
            var questionsList = await PaginatedList<Question>.CreateAsync(questions, pageIndex, pageSize);
            Utils.MinimizeContent(parser, questionsList);
            var homeViewModel = new HomeViewModel
            {
                Questions = questionsList
            };
            return homeViewModel;
        }
    }
}
