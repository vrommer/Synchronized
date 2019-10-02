using Synchronized.Domain;
using Synchronized.Repository.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Logging;

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

        public async override Task<bool> DeleteCommentAsync(string commentId)
        {
            var comment = new Comment()
            {
                Id = commentId
            };
            try
            {
                _context.Attach(comment);
                _context.Remove(comment);
                int x = await _context.SaveChangesAsync();
                return x == 1;
            } catch(DbUpdateException ex)
            {
                _logger.LogDebug(ex.StackTrace);
                return false;
            }
        }
    }
}
