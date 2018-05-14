using Synchronized.Model;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface IPostsRepository : IDataRepository<Post>
    {
        Post FindPostById(string itemId);
        Task<T> FindPostOfType<T>(Expression<Func<T, bool>> predicate) where T: VotedPost;
    }
}
