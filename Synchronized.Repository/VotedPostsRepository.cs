using Microsoft.EntityFrameworkCore;
using Synchronized.Domain;
using Synchronized.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronized.Repository
{
    public class VotedPostsRepository<TEntity>: PostsRepository<TEntity>, IVotedPostRepository<TEntity> where TEntity: VotedPost
    {
        public VotedPostsRepository(DbContext context): base(context)
        {
        }

        public async override Task<TEntity> GetById(string postId)
        {
            var post = await _set
                .AsNoTracking()
                .Include(p => p.Votes)
                .Include(p => p.PostFlags)
                .Where(p => p.Id == postId)
                .SingleOrDefaultAsync();
            return post;
        }
    }
}
