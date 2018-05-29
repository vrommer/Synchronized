using Synchronized.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Synchronized.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Synchronized.Repository
{
    public class DataRepository<T> : IDataRepository<T> where T : class, IEntity
    {
        protected DbContext _context;
        protected DbSet<T> _set;

        public DataRepository(DbContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string entityId)
        {
            throw new NotImplementedException();
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

        public virtual List<T> GetPage(int pageNumber, int pageSize, string searchTerm, string filter)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T Entity)
        {
            throw new NotImplementedException();
        }
    }
}
