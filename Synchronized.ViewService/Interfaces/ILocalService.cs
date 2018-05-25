using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel;
using System.Threading.Tasks;

namespace Synchronized.ViewServices.Interfaces
{
    public interface ILocalService
    {
        Task<PaginatedList<QuestionForHomePage>> GetHomePageModel(int PageNumber);
        Task<PaginatedList<ViewModel.QuestionsViewModels.QuestionForQuestionsPage>> GetQuestionsIndexPageModel(int pageNumber, string sortOrder, string searchTerm);
        Task<ViewModel.QuestionsViewModels.QuestionForDetailsPage> GetQuestionDetailsPageModel(string questionId);
    }
}
