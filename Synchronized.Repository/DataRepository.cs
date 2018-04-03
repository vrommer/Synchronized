using Microsoft.EntityFrameworkCore;
using Synchronized.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Synchronized.Model;

namespace Synchronized.Repository.Repositories
{
    public class DataRepository<TModel> : IDataRepository<TModel> where TModel : class, IEntity
    {
        internal DbContext _context;
        internal DbSet<TModel> _dbSet;

        public DataRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TModel>();
        }

        public void Add(TModel item)
        {
            _dbSet.Add(item);
        }

        public void Delete(string itemId)
        {
            var item =_dbSet.Find(itemId);
            _dbSet.Remove(item);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TModel> FindBy(Expression<Func<TModel, bool>> predicate)
        {
            IEnumerable<TModel> results = _dbSet.AsNoTracking()
                .Where(predicate).ToList();
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

        public void Update(TModel item)
        {
            _dbSet.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
