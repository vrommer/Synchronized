using Synchronized.ViewServices.Interfaces;
using System;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel;
using Synchronized.ViewModel.QuestionsViewModel;
using System.Threading.Tasks;
using Synchronized.Core.Interfaces;
using Synchronized.ViewModelFactories;

namespace Synchronized.ViewServices
{
    public class LocalService : ILocalService
    {
        public LocalService(ICoreService service, ViewModelFactory factory)
        {

        }

        public Task<PaginatedList<QuestionForHomePage>> GetHomePageModel(int PageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionForQuestionsPage> GetQuestionsDetailsPageModel(string questionId)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<QuestionForQuestionsPage>> GetQuestionsIndexPageModel(int? pageNumber, string sortOrder, string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
