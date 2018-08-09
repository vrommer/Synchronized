using Synchronized.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    /// <summary>
    /// This interface defines a repository for working with Questions and Questions related Data such as Comments, Answers and QuestionTags.
    /// </summary>
    public interface IQuestionsRepository: IPostsRepository<Question>
    {
        /// <summary>
        /// Get a page of Questions.
        /// </summary>
        /// <param name="pageIndex">The index of the page.</param>
        /// <param name="pageSize">The size of a page.</param>
        /// <returns>A List of Questions from the Database.</returns>
        Task<List<Question>> GetPageAsync(int pageIndex, int pageSize);

        /// <summary>
        /// Get a Page of Questions that are marked for Review or that have Answers that are marked for Review.
        /// </summary>
        /// <param name="pageIndex">The index of the page.</param>
        /// <param name="pageSize">The size of a Page.</param>
        /// <returns>A hashset of Questions that are marked for review or have Answers that are marked for review.</returns>
        Task<HashSet<Question>> GetReviewPage(int pageIndex, int pageSize);

        /// <summary>
        /// Get a Question from the Database by it's Id.
        /// </summary>
        /// <param name="id">The Id of the required Question</param>
        /// <returns>A Question from the Database.</returns>
        Task<Question> GetQuestionById(string id);

        /// <summary>
        /// Get an Answer from the Database by Id.
        /// </summary>
        /// <param name="postId">The Id of the required Answer.</param>
        /// <returns>An Answer with the Id postId</returns>
        Task<Answer> GetAnswerById(string postId);

        /// <summary>
        /// Get a Comment from the Database by Id.
        /// </summary>
        /// <param name="commentId">The id of the required Comment.</param>
        /// <returns>A comment from the Database</returns>
        Task<Comment> GetCommentById(string commentId);

        /// <summary>
        /// Update an Answer in the Database.
        /// </summary>
        /// <param name="answer">The answer we wish to Update.</param>
        /// <returns></returns>
        Task UpdateAnswerAsync(Answer answer);

        /// <summary>
        /// Get a QuestionTag by Id.
        /// </summary>
        /// <param name="tagId">The id of the required Tag.</param>
        /// <returns>A Tag from the database.</returns>
        Task<Tag> GetQuestionTagById(string tagId);

        /// <summary>
        /// Get the number of Questions that are in the Review Queue.
        /// </summary>
        /// <returns></returns>
        int GetReviewCount();
    }
}
