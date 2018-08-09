using Synchronized.Domain;
using Synchronized.Repository.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Synchronized.Repository
{
    /// <summary>
    /// A generic repository for storing and retreiving Posts to and from the database
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class PostsRepository<TModel> : DataRepository<TModel>, IPostsRepository<TModel> where TModel : Post
    {
        public PostsRepository(DbContext context): base(context)
        {
        }

        public async override Task<TModel> GetById(string id) {
            var post = await _set
                .AsNoTracking()
                .Where(p => p.Id == id)
                .SingleOrDefaultAsync();
            return post;
        }
    }
}
