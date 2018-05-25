using Synchronized.Domain;
using Synchronized.Repository.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Synchronized.Repository
{
    public class PostsRepository : DataRepository<Post>, IPostsRepository
    {
        public PostsRepository(DbContext context): base(context)
        {

        }

        public Task<VotedPost> GetVotedPostBy(Expression<Func<VotedPost, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
