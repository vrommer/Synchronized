using Synchronized.ServiceModel;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    /// <summary>
    /// This interface defines methods for working with VotedPosts.
    /// In our System Voted Posts are Questions and Answers.
    /// </summary>
    public interface IVotedPostService: IPostsService<VotedPost>
    {
        /// <summary>
        /// This method is for flagging VotedPosts.
        /// </summary>
        /// <param name="postId">The post we wish to flag.</param>
        /// <param name="userId">The Id of the user who wishes to flag the post.</param>
        /// <param name="userPoints">The number of points the user who wishes to flag the post has.</param>
        /// <returns>Boolean indicator of whether the flagging finished successfully.</returns>
        Task<bool> FlagPost(string postId, string userId, int userPoints);

        /// <summary>
        /// This method is for getting a VotedPost by a predicate.
        /// </summary>
        /// <param name="predicate">A delegate function. A Lambda can be used.</param>
        /// <returns>VotedPost.</returns>
        Task<VotedPost> GetVotedPostBy(Expression<Func<VotedPost, bool>> predicate);

        /// <summary>
        /// This method is a convenience method for Commenting on VotedPosts.
        /// </summary>
        /// <param name="postId">The Id of the post we wish to comment</param>
        /// <param name="commentBody">The body of the post we wish to comment</param>
        /// <param name="userId">the Id of the Commenter.</param>
        /// <param name="userName">The name of the Commenter</param>
        /// <param name="userPoints">The number of Points Commenter has.</param>
        /// <returns>The newely created Comment.</returns>
        Task<Comment> CommentOnPost(string postId, string commentBody, string userId, string userName, int userPoints);
    }
}
