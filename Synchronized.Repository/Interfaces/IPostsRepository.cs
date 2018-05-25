using Synchronized.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface IPostsRepository: IDataRepository<Post>
    {
        Task<VotedPost> GetVotedPostBy(Expression<Func<VotedPost, bool>> predicate);
    }
}
