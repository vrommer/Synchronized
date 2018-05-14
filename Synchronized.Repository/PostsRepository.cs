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


        public async Task<TPost> FindPostOfType<TPost>(Expression<Func<TPost, bool>> predicate) where TPost: VotedPost
        {
            var post = await _dbSet
                .AsNoTracking()
                .OfType<TPost>()                
                .Include(cp => cp.Votes)
                .Where(predicate)
                .SingleOrDefaultAsync();
            return post;
        }
    }
}
