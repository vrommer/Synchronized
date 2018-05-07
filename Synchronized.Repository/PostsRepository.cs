using Microsoft.EntityFrameworkCore;
using Synchronized.Model;
using Synchronized.Repository.Interfaces;
using Synchronized.Repository.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Repository
{
    public class PostsRepository : DataRepository<Post>, IPostsRepository
    {

        public PostsRepository(DbContext context) : base(context)
        {

        }

        public Post FindPostById(string itemId)
        {
            var post = _dbSet
                .Include(p => p.PostFlags)
                .Include(p => p.DeleteVotes)
                .AsNoTracking()
                .SingleOrDefault(p => p.Id.Equals(itemId));

            return post;
        }


        public async Task<T> FindPostOfType<T>(Expression<Func<T, bool>> predicate) where T: CommentedPost
        {
            var post = await _dbSet
                .OfType<T>()
                .AsNoTracking()
                .Include(cp => cp.Votes)
                .Where(predicate)
                .SingleOrDefaultAsync();
            return post;
        }
    }
}
