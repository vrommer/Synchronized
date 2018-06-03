using Microsoft.EntityFrameworkCore;
using Synchronized.Domain;
using Synchronized.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Synchronized.Repository
{
    public class VotedPostsRepository: PostsRepository<VotedPost>, IVotedPostRepository
    {
        public VotedPostsRepository(DbContext context): base(context)
        {
        }

        public async override Task<VotedPost> GetById(string postId)
        {
            var post = await _set
                .AsNoTracking()
                .Include(p => p.Votes)
                .Include(p => p.PostFlags)
                .Include(p => p.DeleteVotes)
                .Include(p => p.Comments)
                .Where(p => p.Id == postId)
                .SingleOrDefaultAsync();
            return post;
        }
    }
}
