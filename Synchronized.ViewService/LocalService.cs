using Synchronized.ViewServices.Interfaces;
using System;
using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel;
using Synchronized.ViewModel.QuestionsViewModels;
using Synchronized.Core.Interfaces;
using Synchronized.ViewModelFactories.Interfaces;
using Synchronized.UI.Utilities.Interfaces;

namespace Synchronized.ViewServices
{
    public class LocalService : ILocalService
    {
        private int pageSize = 20;
        private IQuestionsService _questionsService;
        private IDataConverter _converter;
        private IViewModelFactory _factory;

        public LocalService(IQuestionsService questionsService, IDataConverter converter, IViewModelFactory factory)
        {
            _questionsService = questionsService;
            _converter = converter;
            _factory = factory;
        }

        public async Task<PaginatedList<QuestionForHomePage>> GetHomePageModel(int pageIndex)
        {
            var questions = await _questionsService.GetPage(pageIndex, pageSize);
            var questionsPage = _factory.GetPaginatedList<QuestionForHomePage>(questions.TotalSize, pageIndex, pageSize);
            questions.ForEach(q => {
                var viewModelQuestion = ((IHomeViewConverter)_converter).Convert(q);
                questionsPage.Add(viewModelQuestion);
            });
            return questionsPage;
        }

        public async Task<QuestionForDetailsPage> GetQuestionDetailsPageModel(string questionId, string userId)
        {
            var question = await _questionsService.ViewQuestion(questionId, userId);
            var questionForDetailsPage = ((IDetailsConverter)_converter).Convert(question);
            return questionForDetailsPage;
        }

        public async Task<PaginatedList<QuestionForQuestionsPage>> GetQuestionsIndexPageModel(int pageIndex, string searchTerm, string sortOrder)
        {
            var questions = await _questionsService.GetPage(pageIndex, pageSize, searchTerm, sortOrder);
            var questionsPage = _factory.GetPaginatedList<QuestionForQuestionsPage>(questions.Count, pageIndex, pageSize);
            questions.ForEach(q => {
                var question = _factory.GetQuestionForQuestionsPage();
                questionsPage.Add(((IQuestionsConverter)_converter).Convert(q));
            });
            return questionsPage;
        }
    }
}
