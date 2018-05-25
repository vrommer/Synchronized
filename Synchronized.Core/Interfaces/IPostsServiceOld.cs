using Synchronized.Domain;
using System.Threading.Tasks;
using SharedLib.Infrastructure.Constants;
using System.Collections.Generic;

namespace Synchronized.Core.Interfaces
{
    public interface IPostsServiceOld : IDataServiceOld<ServiceModel.Post, Post>
    {
        object CommentOnPost(ApplicationUser user, ServiceModel.Comment comment);
        Task VoteForPost(ServiceModel.VotedPost post, ApplicationUser user, VoteType upVote);
        ICollection<T> GetPostsPage<T>();
    }
}
