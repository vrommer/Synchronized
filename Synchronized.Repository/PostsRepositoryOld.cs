using Microsoft.EntityFrameworkCore;
using Synchronized.Domain;
using Synchronized.Repository.Interfaces;
using Synchronized.Repository.Repositories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Repository
{
    public class PostsRepositoryOld<TModel> : DataRepositoryOld<TModel>, IPostsRepositoryOld<TModel> where TModel: Post
    {

        public PostsRepositoryOld(DbContext context) : base(context)
        {
        }

        public Task<VotedPost> GetVotedPostBy(Expression<Func<VotedPost, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        // ----------------- OLD METHODS -----------------
        //public Post FindPostById(string itemId)
        //{
        //    var post = _dbSet
        //        .Include(p => p.PostFlags)
        //        .Include(p => p.DeleteVotes)
        //        .AsNoTracking()
        //        .SingleOrDefault(p => p.Id.Equals(itemId));

        //    return post;

        //}

        //public Task AddAsync(TModel entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Delete(string itemId)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<VotedPost> GetVotedPostBy(Expression<Func<VotedPost, bool>> predicate)
        //{
        //    var post = await _dbSet
        //         .AsNoTracking()
        //         .OfType<VotedPost>()
        //         .Include(cp => cp.Votes)
        //         .Where(predicate)
        //         .SingleOrDefaultAsync();
        //    return post;
        //}

        //public TModel GetById(string itemId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> GetCount()
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<TModel> GetPage(int pageIndex, int pageSize)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Update(TModel item)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<TModel> GetBy(Expression<Func<TModel, bool>> predicate)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual Task<TModel> GetByIdAsync(string itemId)
        //{
        //    throw new NotImplementedException();
        //}

        // ----------------- NEW METHODS -----------------


    }
}
