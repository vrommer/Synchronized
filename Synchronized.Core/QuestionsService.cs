using Synchronized.Core.Interfaces;
using Synchronized.Repository.Interfaces;
using Synchronized.Core.Utilites;
using Synchronized.ServiceModel;
using Synchronized.Core.Utilities.Interfaces;
using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;
using UtilsLib.HtmlUtils.HtmlParser;
using Synchronized.Core.Factories.Interfaces;
using System.Collections.Generic;

namespace Synchronized.Core
{
    public class QuestionsService : PostsService<Domain.Question, Question>, IQuestionsService
    {
        private HtmlParser _parser;

        public QuestionsService(IQuestionsRepository repo, IServiceModelFactory factory, IDataConverter converter, HtmlParser parser) : base(repo, factory, converter)
        {
            _parser = parser;
        }

        public async Task<PaginatedList<Question>> GetPage(int pageIndex, int pageSize)
        {
            return await GetQuestionsPage(pageIndex, pageSize);
        }

        public async override Task<PaginatedList<Question>> GetPage(int pageIndex, int pageSize, string sortOrder, string searchString)
        {
            return await GetQuestionsPage(pageIndex, pageSize, sortOrder, searchString);
        }

        public async override Task<Question> GetById(string id)
        {
            var domainQuestion = await ((IQuestionsRepository)_repo).GetById(id);
            var question = _converter.Convert(domainQuestion);
            return question;
        }

        private async Task<PaginatedList<Question>> GetQuestionsPage(int pageIndex, int pageSize, string sortOrder = null, string searchString = null)
        {
            List<Domain.Question> domainQuestions; ;
            if (sortOrder == null)
            {
                domainQuestions = await ((IQuestionsRepository)_repo).GetPageAsync(pageIndex, pageSize);
            } else
            {
                domainQuestions = ((IQuestionsRepository)_repo).GetPage(pageIndex, pageSize, sortOrder, searchString);
            }
            var questions = _converter.Convert(domainQuestions);
            if (sortOrder != null)
            {
                Utils.MinimizeContent(_parser, questions);
            }
            var questionsPage = _factory.GetQuestionsList(questions, _repo.GetCount(), pageSize, pageIndex);
            return questionsPage;
        }
    }
}
