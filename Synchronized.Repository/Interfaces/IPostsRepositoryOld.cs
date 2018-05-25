using Synchronized.Domain;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface IPostsRepositoryOld<TModel> : IDataRepositoryOld<TModel> where TModel: Post
    {
        //---------------------Should Be Removed------------------------

        //---------------------Used Methods------------------------
        Task<VotedPost> GetVotedPostBy(Expression<Func<VotedPost, bool>> predicate);
    }
}
