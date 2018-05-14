using Synchronized.Model;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using Synchronized.ServiceModel;

namespace Synchronized.Core.Interfaces
{
    public interface IPostsService : IDataService<ServiceModel.Post, Model.Post>
    {
        Task VoteUpPost(ServiceModel.VotedPost post, ApplicationUser user);
        Task VoteDownPost(ServiceModel.VotedPost post, ApplicationUser user);
        object CommentOnPost(ApplicationUser user, ServiceModel.Comment comment);
    }
}
