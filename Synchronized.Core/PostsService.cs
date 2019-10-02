using Synchronized.Core.Interfaces;
using Synchronized.Repository.Interfaces;
using Synchronized.Core.Utilities.Interfaces;
using Synchronized.Core.Factories.Interfaces;
using Synchronized.ServiceModel;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using Synchronized.SharedLib;

namespace Synchronized.Core
{
    /// <summary>
    /// This is a Generic PostsService.
    /// </summary>
    /// <typeparam name="TEntity">User defined.</typeparam>
    /// <typeparam name="TServiceModel">User defined.</typeparam>
    public class PostsService<TEntity, TServiceModel> : DataService<TEntity, TServiceModel>, IPostsService<TServiceModel> 
        where TServiceModel : Post
        where TEntity : Domain.Post
    {

        public PostsService(IPostsRepository<TEntity> repo, IServiceModelFactory factory, IDataConverter converter, ILogger<PostsService<TEntity, TServiceModel>> logger)  : base(repo, factory, converter, logger) 
        {
        }

        public virtual async Task<bool> DeletePost(string postId, string userId, int userPoints)
        {
            var post = await _repo.GetById(postId);
            if (CanDelete(userId, userPoints))
            {
                await _repo.DeleteAsync(post);
                return true;
            }
            return false;
        }

        private bool CanDelete(string userId, int userPoints)
        {
            return !String.IsNullOrWhiteSpace(userId) && Constants.DELETE_POINST <= userPoints;
        }

        public async Task<bool> DeleteComment(
            string commentId,
            string publisherId,
            string votedPostPublisherId,
            string userId
        )
        {
            var canDelete = (userId.Equals(publisherId)
                || userId.Equals(votedPostPublisherId));
            if (canDelete)
            {
                return await _repo.DeleteCommentAsync(commentId);
            }
            return false;
        }
    }
}
