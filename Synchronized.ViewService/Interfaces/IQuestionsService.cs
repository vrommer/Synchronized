using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel;
using Synchronized.ViewModel.QuestionsViewModels;
using System.Threading.Tasks;

namespace Synchronized.ViewServices.Interfaces
{
    public interface IQuestionsService
    {
        Task<PaginatedList<QuestionForHomePage>> GetHomePageModel(int PageNumber);
        Task<PaginatedList<QuestionForQuestionsPage>> GetQuestionsIndexPageModel(int pageNumber, string sortOrder, string searchTerm);
        Task<QuestionForDetailsPage> GetQuestionDetailsPageModel(string questionId, string userId);
        Task<string> AskQuestion(AskViewModel question, string userId);
        Task AnswerQuestion(AnswerViewModel answer, string userId, string questionId);
    }
}
