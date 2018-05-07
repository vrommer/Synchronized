using Synchronized.Core.Interfaces;
using Synchronized.Model;
using Synchronized.Repository.Interfaces;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;

namespace Synchronized.Core
{
    public class PostsService : DataService<Post>, IPostsService
    {
        public PostsService(
            IPostsRepository postsRepo) : base(postsRepo)
        {
        }

        public Post FindPostById(string itemId)
        {
            return ((IPostsRepository)_repo).FindPostById(itemId);
        }

        public async Task<TPost> FindtPostOfType<TPost>(Expression<Func<TPost, bool>> predicate) where TPost : CommentedPost
        {
            return await ((IPostsRepository)_repo).FindPostOfType(predicate);
        }
    }
}
