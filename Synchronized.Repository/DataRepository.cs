using Microsoft.EntityFrameworkCore;
using Synchronized.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Synchronized.Model;
using System.Threading.Tasks;

namespace Synchronized.Repository.Repositories
{
    public class DataRepository<TModel> : IDataRepository<TModel> where TModel : class, IEntity
    {
        protected DbContext _context;
        protected DbSet<TModel> _dbSet;

        public DataRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TModel>();
        }

        public void Add(TModel item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public int CountItems()
        {
            throw new NotImplementedException();
        }

        public void Delete(string itemId)
        {
            var item =_dbSet.Find(itemId);
            _dbSet.Remove(item);
        }

        public IQueryable<TModel> FindBy(Expression<Func<TModel, bool>> predicate)
        {
            IQueryable<TModel> results = _dbSet.AsNoTracking()
                .Where(predicate);
            return results;
        }

        public TModel FindById(string itemId)
        {
            return _dbSet.AsNoTracking().SingleOrDefault(e => e.Id.Equals(itemId));
        }

        public IEnumerable<TModel> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
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
    }
}
