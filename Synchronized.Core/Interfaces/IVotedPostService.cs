using SharedLib.Infrastructure.Constants;
using Synchronized.ServiceModel;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IVotedPostService: IPostsService<VotedPost>
    {
        Task<bool> FlagPost(string postId, string userId, int userPoints);
        //Task<bool> DeletePost(string postId, string userId);
        Task<VotedPost> GetVotedPostBy(Expression<Func<VotedPost, bool>> predicate);
        Task<Comment> CommentOnPost(string postId, string commentBody, string userId, string userName, int userPoints);
    }
}
