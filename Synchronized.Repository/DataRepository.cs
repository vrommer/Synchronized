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

        public Task<IQueryable<T>> GetByAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> GetById(string entityId)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<T>> GetPageAsync(int pageNumber, int pageSize, string searchTerm, string filter)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T Entity)
        {
            throw new NotImplementedException();
        }
    }
}
