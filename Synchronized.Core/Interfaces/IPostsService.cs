using Synchronized.ServiceModel;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    /// <summary>
    /// This is a generic Posts service. It implements the IDataService interface 
    /// for the generic data type TEntity, restricting it to be of the base abstract type Post.
    /// This interface defines methods that are relevant for all Types of Posts.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IPostsService<TEntity>: IDataService<TEntity> where TEntity: Post
    {
        /// <summary>
        /// This method is for deleting items of type Post from the System.
        /// </summary>
        /// <param name="postId">The Id of the post to be deleted.</param>
        /// <param name="userId">The Id of the user who wishes to delete the post.</param>
        /// <param name="userPoints">The number of points of the user who wishes to delete the post./</param>
        /// <returns>Boolean indicator of whether the method has finished successfully.</returns>
        Task<bool> DeletePost(string postId, string userId, int userPoints);

        /// <summary>
        /// This method is for deleting comments
        /// </summary>
        /// <param name="commentId">The id of the comment</param>
        /// <param name="publisherId">The id of the comment publisher</param>
        /// <param name="votedPostPublisherId">Id of the comment parent post publisher</param>
        /// <param name="userId">The current user</param>
        /// <returns></returns>
        Task<bool> DeleteComment(string commentId, string publisherId, string votedPostPublisherId, string userId);
    }
}
