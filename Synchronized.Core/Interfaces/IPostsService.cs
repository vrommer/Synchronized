using Synchronized.ServiceModel;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IPostsService<T>: IDataService<T> where T: Post
    {
        Task<VotedPost> GetVotedPostBy(Expression<Func<VotedPost, bool>> predicate);
    }
}
