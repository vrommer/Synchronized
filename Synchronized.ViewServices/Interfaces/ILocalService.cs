using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.ViewServices.Interfaces
{
    public interface ILocalService
    {
        Task<PaginatedList<QuestionForHomePage>> GetHomePageModel(int PageNumber);
        Task<PaginatedList<ViewModel.QuestionsViewModel.QuestionForQuestionsPage>> GetQuestionsIndexPageModel(int? pageNumber, string sortOrder, string searchTerm);
        Task<ViewModel.QuestionsViewModel.QuestionForQuestionsPage> GetQuestionsDetailsPageModel(string questionId);
    }
}
