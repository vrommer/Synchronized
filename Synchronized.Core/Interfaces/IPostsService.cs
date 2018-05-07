using Synchronized.Model;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;

namespace Synchronized.Core.Interfaces
{
    public interface IPostsService : IDataService<Post>
    {
        Post FindPostById(string itemId);
        Task<TPost> FindtPostOfType<TPost>(Expression<Func<TPost, bool>> predicate) where TPost: CommentedPost;
    }
}
