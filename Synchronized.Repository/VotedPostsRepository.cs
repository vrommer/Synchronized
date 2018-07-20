using Microsoft.EntityFrameworkCore;
using Synchronized.Domain;
using Synchronized.Repository.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
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
            VotedPost post;
            var postQuery = _set
                .AsNoTracking()
                .Include(p => p.Votes)
                .Include(p => p.PostFlags)
                .Include(p => p.DeleteVotes)
                .Include(p => p.Comments)
                .Where(p => p.Id == postId);
            post = await postQuery.SingleOrDefaultAsync();
            // TODO Doesn't work!!!
            //----------------------
            //if (post.GetType().Equals(typeof(Question)))
            //{
            //    var item = Expression.Parameter(typeof(Question), "question");
            //    var prop = Expression.Property(item, "Subscriptions");
            //    //var soap = Expression.Constant("Soap");
            //    //var equal = Expression.Equal(prop, soap);
            //    var lambda = Expression.Lambda<Func<VotedPost, System.Collections.Generic.List<Subscription>>>(prop, item);
            //    var result = await postQuery.Include(lambda).SingleOrDefaultAsync();
            //}
            return post;
        }
    }
}
