using Microsoft.EntityFrameworkCore;
using Synchronized.Repository.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using Synchronized.Domain;
using System.Threading.Tasks;

namespace Synchronized.Repository.Repositories
{
    public class DataRepositoryOld<TModel> : IDataRepositoryOld<TModel> where TModel : class, IEntity
    {
        protected DbContext _context;
        protected DbSet<TModel> _dbSet;

        public DataRepositoryOld(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TModel>();
        }

        //----------------------- ShouldBeRemoved ----------------------
        public void Add(TModel item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public IQueryable<TModel> GetBy(Expression<Func<TModel, bool>> predicate)
        {
            IQueryable<TModel> results = _dbSet.AsNoTracking()
                .Where(predicate);
            return results;
        }

        public virtual void Delete(string itemId)
        {
            var item = _dbSet.Find(itemId);
            _dbSet.Remove(item);
        }

        public async Task<TModel> GetByIdAsync(string itemId)
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync(e => e.Id.Equals(itemId));
        }

        public async Task<int> GetCount()
        {
            int count = await _dbSet.CountAsync();
            return count;
        }

        public void Update(TModel item)
        {
            _dbSet.Update(item);
            _context.SaveChanges();
        }

        public IQueryable<TModel> GetPage(int pageIndex, int pageSize)
        {
            return _dbSet.AsNoTracking().Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public async Task AddAsync(TModel model)
        {
            await _dbSet.AddAsync(model);
            await _context.SaveChangesAsync();
        }
        //------------------------ UsedMethods ------------------------
        //public async Task AddAsync(TModel item)
        //{
        //    _dbSet.Add(item);
        //    await _context.SaveChangesAsync();
        //}

        public async Task DeleteAsync(string itemId)
        {
            var item = _dbSet.Find(itemId);
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public virtual Task<IQueryable<TModel>> GetByAsync(Expression<Func<TModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TModel>> GetPageAsync(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TModel item)
        {
            throw new NotImplementedException();
        }
    }
}
