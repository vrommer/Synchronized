using Synchronized.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Synchronized.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Synchronized.Repository
{
    public class DataRepository<T> : IDataRepository<T> where T : class, IEntity
    {
        protected DbContext _context;
        protected DbSet<T> _set;
        protected ILogger<Object> _logger;

        public DataRepository(DbContext context, ILogger<DataRepository<T>> logger)
        {
            _context = context;
            _set = context.Set<T>();
            _logger = logger;
        }

        public DataRepository(DbContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }

        public async Task<string> AddAsync(T entity)
        {
            _set.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> results = _set.AsNoTracking()
                .Where(predicate);
            return results;
        }

        public virtual Task<T> GetById(string entityId)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return _set.Count();
        }

        public virtual List<T> GetPage(int pageNumber, int pageSize, string searchTerm, string sortOrder)
        {
            throw new NotImplementedException();
        }

        //public async Task UpdateAsync(T Entity)
        //{
        //    _set.Update(Entity);
        //    await _context.SaveChangesAsync();
        //}

        public virtual async Task UpdateAsync<TEntity>(TEntity Entity) where TEntity: class
        {
            _context.Set<TEntity>().Update(Entity);
            await _context.SaveChangesAsync();
        }
    }
}
