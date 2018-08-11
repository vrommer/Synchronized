using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel;
using Synchronized.ViewModel.QuestionsViewModels;
using System.Threading.Tasks;

namespace Synchronized.ViewServices.Interfaces
{
    /// <summary>
    /// This interface defines methods for working with Questions in the ViewModel Context.
    /// </summary>
    public interface IQuestionsService
    {
        /// <summary>
        /// A method for retreiving a PaginatedList of QuestionForHomePage.
        /// </summary>
        /// <param name="pageNumber">The number of the required Page.</param>
        /// <returns>An instance of PaginatedList of QuestionForHomePage.</returns>
        Task<PaginatedList<QuestionForHomePage>> GetHomePageModel(int PageNumber);

        /// <summary>
        /// A method for retreiving a PaginatedList of QuestionForQuestionsPage.
        /// </summary>
        /// <param name="pageNumber">The number of required page.</param>
        /// <param name="sortOrder">The order the page should be sorted by.</param>
        /// <param name="searchTerm">A search term to be present in the QuestionForQuestionsPage.</param>
        /// <returns>An instance of PaginatedList of QuestionForQuestionsPage.</returns>
        /// <returns></returns>
        Task<PaginatedList<QuestionForQuestionsPage>> GetQuestionsIndexPageModel(int pageNumber, 
            string sortOrder, string searchTerm);

        /// <summary>
        /// </summary>
        /// <param name="questionId">The Id of the Question.</param>
        /// <param name="userId">The Id of the Viewing User.</param>
        /// <returns>An instance of QuestionForDetailsPage.</returns>
        Task<QuestionForDetailsPage> GetQuestionDetailsPageModel(string questionId, string userId);

        /// <summary>
        /// A method for Asking Questions.
        /// </summary>
        /// <param name="question"></param>
        /// <param name="userId"></param>
        /// <returns>The new Question Id if the action is successful or empty string.</returns>
        Task<string> AskQuestion(AskViewModel question, string userId);

        /// <summary>
        /// A method for Asnwering Questions.
        /// </summary>
        /// <param name="answer">The answer for the Question.</param>
        /// <param name="userId">The Id of the Answerer.</param>
        /// <param name="questionId">The Id of the answered Question.</param>
        /// <returns></returns>
        Task AnswerQuestion(AnswerViewModel answer, string userId, string questionId);

        /// <summary>
        /// A method for retreiving PaginatedList of QuestionForQuestionsPage.
        /// </summary>
        /// <param name="currentPage">The number of the required Page.</param>
        /// <returns>A Paginated List of QuestionForQuestionsPage</returns>
        Task<PaginatedList<QuestionForQuestionsPage>> GetPageForReview(int currentPage);
    }
}
