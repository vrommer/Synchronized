using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;
using System.Collections.Generic;

namespace Synchronized.Core.Factories.Interfaces
{
    /// <summary>
    /// This is a factory for creating objects of the ViewModel type.
    /// </summary>
    public interface IServiceModelFactory
    {
        /// <summary>
        /// Create a Question.
        /// </summary>
        /// <returns>Question</returns>
        Question GetQuestion();

        /// <summary>
        /// Create an Answer.
        /// </summary>
        /// <returns>Answer</returns>
        Answer GetAnswer();

        /// <summary>
        /// Create a Comment.
        /// </summary>
        /// <returns>Comment</returns>
        Comment GetComment();

        /// <summary>
        /// Create a List of Questions.
        /// </summary>
        /// <returns>A new PaginatedList of Question.</returns>
        PaginatedList<Question> GetQuestionsPage(int totalSize, int pageIndex, int pageSize);

        /// <summary>
        /// Create a PaginatedList of Questions from an existing List of Questions.
        /// </summary>
        /// <param name="questions">The original list of Questions</param>
        /// <param name="totalSize">The total number of Questions.</param>
        /// <param name="pageIndex">The index of a page.</param>
        /// <param name="pageSize">The size of a page.</param>
        /// <returns>Paginated Paginated List of Questions.</returns>
        PaginatedList<Question> GetQuestionsPage(List<Question> questions, int totalSize, int pageIndex, int pageSize);

        /// <summary>
        /// Get a list of Questions. Returns a simple list of Questions.
        /// </summary>
        /// <returns>New List of Questions.</returns>
        List<Question> GetQuestionsList();

        /// <summary>
        /// Get a list of Tags.
        /// </summary>
        /// <returns>A new list of Tags.</returns>
        List<Tag> GetTagsList();

        /// <summary>
        /// Get a user.
        /// </summary>
        /// <returns>A new User.</returns>
        User GetUser();

        /// <summary>
        /// Creates 
        /// </summary>
        /// <returns></returns>
        List<User> GetUsersList();

        /// <summary>
        /// Create a List of Ansers.
        /// </summary>
        /// <returns>A new List of Answers.</returns>
        List<Answer> GetAnswersList();

        /// <summary>
        /// Create a List of Comments.
        /// </summary>
        /// <returns>A new List of Comments.</returns>
        List<Comment> GetCommentsList();

        /// <summary>
        /// Returns a Voted Post.
        /// </summary>
        /// <returns>A new Voted Post.</returns>
        VotedPost GetVotedPost();

        /// <summary>
        /// Returns a New Tag
        /// </summary>
        /// <returns>A new Tag.</returns>
        Tag GetTag();

        /// <summary>
        ///  Return a PaginatedList of Tags
        /// </summary>
        /// <param name="tags">The original List of Tags.</param>
        /// <param name="count">The total number of Tags.</param>
        /// <param name="pageSize">The size of the Page.</param>
        /// <param name="pageIndex">The Index of the Page.</param>
        /// <returns>A new Paginated List of Tags with data.</returns>
        PaginatedList<Tag> GetTagsPage(List<Tag> tags, int count, int pageSize, int pageIndex);

        /// <summary>
        /// A generic method that create an Item of any Type.
        /// </summary>
        /// <typeparam name="T">The type we wish to create.</typeparam>
        /// <returns>A new instance of T</returns>
        T GetOfType<T>();

        /// <summary>
        /// Creates a Paginated List of Users.
        /// </summary>
        /// <param name="users">A List of User.</param>
        /// <param name="count">The total number of Users.</param>
        /// <param name="pageSize">The size of a page.</param>
        /// <param name="pageIndex">The number of the page.</param>
        /// <returns>A new Paginated List of Users with data.</returns>
        PaginatedList<User> GetUsersPage(List<User> users, int count, int pageSize, int pageIndex);
    
    }
}
