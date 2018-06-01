using SharedLib.Infrastructure.Constants;
using Synchronized.ServiceModel;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IPostsService<TEntity>: IDataService<TEntity> where TEntity: Post
    {
        Task<VotedPost> GetVotedPostBy(Expression<Func<VotedPost, bool>> predicate);
        Task<bool> FlagPost(string postId, string userId);
        Task<bool> DeletePost(string postId, string userId);
        Task<T> VoteForPost<T>(string postId, VoteType voteType, string userId) where T : VotedPost;
        Task<T> CommentOnPost<T>(string postId, string userId, string commentBody) where T : VotedPost;
    }
}
