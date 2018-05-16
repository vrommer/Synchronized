using Synchronized.Model;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using Synchronized.ServiceModel;
using SharedLib.Infrastructure.Constants;

namespace Synchronized.Core.Interfaces
{
    public interface IPostsService : IDataService<ServiceModel.Post, Model.Post>
    {
        object CommentOnPost(ApplicationUser user, ServiceModel.Comment comment);
        Task VoteForPost(ServiceModel.VotedPost post, ApplicationUser user, VoteType upVote);
    }
}
