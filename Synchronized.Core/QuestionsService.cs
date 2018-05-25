using Synchronized.Core.Interfaces;
using Synchronized.Repository.Interfaces;
using Synchronized.Core.Utilites;
using Synchronized.ServiceModel;
using Synchronized.Core.Utilities.Interfaces;
using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;
using UtilsLib.HtmlUtils.HtmlParser;

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
            var domainQuestions = await((IQuestionsRepository)_repo).GetPageAsync(pageIndex, pageSize);
            var questions = _factory.GetQuestionsList(_repo.GetCount(), pageIndex, pageSize);
            domainQuestions.ForEach(q => {
                var question = _factory.GetQuestion();
                questions.Add(_converter.Convert(q, question));
            });
            Utils.MinimizeContent(_parser, questions);
            return questions;
        }

        public async override Task<PaginatedList<Question>> GetPage(int pageIndex, int pageSize, string sortOrder, string searchString)
        {
            var domainQuestions = await _repo.GetPageAsync(pageIndex, pageSize, sortOrder, searchString);
            var questions = _factory.GetQuestionsList(_repo.GetCount(), pageIndex, pageSize);
            domainQuestions.ForEach(q => {
                var question = _factory.GetQuestion();
                questions.Add(_converter.Convert(q, question));
            });
            return questions;
        }

        public async override Task<Question> GetById(string id)
        {
            var domainQuestion = await ((IQuestionsRepository)_repo).GetById(id);
            var question = _converter.Convert(domainQuestion, _factory.GetQuestion());
            return question;
        }
    }
}
