using Synchronized.Domain;
using Synchronized.Repository.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Synchronized.Repository
{
    public class PostsRepository<TModel> : DataRepository<TModel>, IPostsRepository<TModel> where TModel: Post
    {
        public PostsRepository(DbContext context): base(context)
        {
        }

        public async override Task<TModel> GetById(string postId)
        {
            var post = await _set
                .AsNoTracking()
                .Include(p => p.PostFlags)
                .Where(p => p.Id == postId)
                .SingleOrDefaultAsync();
            return post;
        }
    }
}
