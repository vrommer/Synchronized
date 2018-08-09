using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;
using SharedLib.Infrastructure.Constants;
using Synchronized.ViewModel;
using Synchronized.Domain;

namespace Synchronized.Core.Interfaces
{
    /// <summary>
    /// This is a service for working with Posts of type ServiceModel.Question.
    /// This interface defines methods for working with Question in the context of Business layer   
    /// </summary>
    public interface IQuestionsService : IPostsService<ServiceModel.Question>
    {
        /// <summary>
        /// This method is for retrieving a page of QUestions.
        /// </summary>
        /// <param name="pageNumber">The number of the required page.</param>
        /// <param name="pageSize">Number of Questions per page.</param>
        /// <returns>List of Questions.</returns>
        Task<PaginatedList<ServiceModel.Question>> GetPage(int pageNumber, int pageSize);

        /// <summary>
        /// This method is a convenience method for Voting for Questions.
        /// </summary>
        /// <param name="postId">The id of the Question.</param>
        /// <param name="upVote">The type of the vote. One of { UpVote, DownVote }</param>
        /// <param name="User">The User who Votes for the Question.</param>
        /// <returns>The Question after Voting.</returns>
        Task<ServiceModel.Question> VoteForQuestion(string postId, VoteType upVote, ApplicationUser User);

        /// <summary>
        /// This method is a convenience method for Voting for Answers.
        /// </summary>
        /// <param name="postId">The id of the Answer.</param>
        /// <param name="upVote">The type of the vote. One of { UpVote, DownVote }</param>
        /// <param name="User">The User who Votes for the Answer.</param>
        /// <returns>The Answer after Voting.</returns>
        Task<ServiceModel.Answer> VoteForAnswer(string postId, VoteType downVote, ApplicationUser User);

        /// <summary>
        /// This method is a convenience method for Viewing Questions.
        /// </summary>
        /// <param name="questionId">The question to be viewed</param>
        /// <param name="userId">The Id of the Viewer of the Question.</param>
        /// <returns>The Question User wishes to View.</returns>
        Task<ServiceModel.Question> ViewQuestion(string questionId, string userId);

        /// <summary>
        /// This is a convenience method for Asking Questions.
        /// </summary>
        /// <param name="question">The newely created Question.</param>
        /// <returns></returns>
        Task<string> AskQuestion(ServiceModel.Question question);

        /// <summary>
        /// This method checks if Tags are Valid.
        /// </summary>
        /// <param name="tags">List of tags for checking.</param>
        /// <returns></returns>
        Task<bool> TagsAreValid(string tags);

        /// <summary>
        /// This async is a convenience method for Answering a Question.
        /// </summary>
        /// <param name="answer">The newely Created Answer.</param>
        /// <param name="questionId">The Id of the Question one wishes to Answer.</param>
        Task AnswerQuestion(ServiceModel.Answer answer, string questionId);

        /// <summary>
        /// This is a convenience method for Accepting Answers. 
        /// </summary>
        /// <param name="answer">The Answer user wishes to accept.</param>
        /// <param name="user">The user who accepts the answer.</param>
        Task AcceptAnswer(AnswerViewModel answer,ApplicationUser user);

        /// <summary>
        /// This convenience method retrieves a page of questions that need reviewing.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PaginatedList<ServiceModel.Question>> ReviewQuestions(int pageIndex, int pageSize);

    }
}
