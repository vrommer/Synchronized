using Synchronized.Core.Interfaces;
using Synchronized.Repository.Interfaces;
using UtilsLib.HtmlUtils.HtmlParser;
using Synchronized.Core.Utilites;
using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.DependencyInjection;
using Synchronized.ServiceModel;
using Microsoft.Extensions.Logging;

namespace Synchronized.Core
{
    public class QuestionsService : DataService<Question, Model.Question>, IQuestionsService, IViewQuestions
    {
        private readonly IModelFactory _factory;
        private readonly IServiceProvider _services;
        private readonly HtmlParser _parser;
        private readonly ILogger<QuestionsService> _logger;

        public QuestionsService(IQuestionsRepository repo, IModelFactory factory, IServiceProvider services, HtmlParser parser, ILogger<QuestionsService> logger) : base(repo)
        {
            _factory = factory;
            _services = services;
            _parser = parser;
            _logger = logger;
        }

        public async Task<PaginatedList<Question>> GetQuestionsPageAsync(int pageIndex, int pageSize)
        {
            var domainQuestions = await ((IQuestionsRepository)_repo).GetQuestionsPageAsync(pageIndex, pageSize);
            var serviceModelQuestions = _services.GetRequiredService<List<Question>>();
            domainQuestions.ForEach(q => serviceModelQuestions.Add(_factory.GetQuestion(q)));
            return await CreatePage(serviceModelQuestions, pageIndex, pageSize);
        }

        public async Task<PaginatedList<Question>> GetQuestionsPageWithUsersAsync(int pageIndex, int pageSize)
        {
            var domainQuestions = ((IQuestionsRepository)_repo).GetQuestionsPageWithUsersAsync(pageIndex, pageSize);
            var serviceModelQuestions = _services.GetRequiredService<List<Question>>();
            domainQuestions.ForEach(q => serviceModelQuestions.Add(_factory.GetQuestion(q)));
            return await CreatePage(serviceModelQuestions, pageIndex, pageSize);
        }

        public Question FindQuestionById(string questionId)
        {
            var domainQuestion = ((IQuestionsRepository)_repo).FindQuestionById(questionId);
            return _factory.GetQuestion(domainQuestion);
        }

        public async Task<PaginatedList<Question>> GetQuestionsPageWithUsersAsync(int pageIndex, int pageSize, string sortOrder, string filter)
        {
            var domainQuestions = ((IQuestionsRepository)_repo).GetQuestionsPageWithUsersAsync(pageIndex, pageSize, sortOrder, filter);
            var serviceModelQuestions = _services.GetRequiredService<List<Question>>();
            domainQuestions.ForEach(q => serviceModelQuestions.Add(_factory.GetQuestion(q)));
            return await CreatePage(serviceModelQuestions, pageIndex, pageSize);
        }

        private async Task<PaginatedList<Question>> CreatePage(List<Question> questions, int pageIndex, int pageSize)
        {
            int count = await _repo.GetCount();
            Utils.MinimizeContent(_parser, questions);
            return new PaginatedList<Question>(questions, count, pageIndex, pageSize);
        }

        public Answer FindAnswerById(string answerId)
        {
            var domainAnswer = ((IQuestionsRepository)_repo).FindAnswerById(answerId);
            return _factory.GetAnswer(domainAnswer);
        }

        public void UpdateQuestion(Question question)
        {
            throw new NotImplementedException();
            //((IQuestionsRepository)_repo).UpdateQuestion(question);
        }

        public void UpdateAnswer(Answer answer)
        {
            throw new NotImplementedException();
            //((IQuestionsRepository)_repo).UpdateAnswer(answer);
        }

        public Task ViewQuestion(string UserId, string questionId)
        {
            throw new NotImplementedException();
        }
    }
}
