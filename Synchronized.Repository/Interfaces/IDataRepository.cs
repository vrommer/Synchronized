using Synchronized.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface IDataRepository<T> where T: class, IEntity
    {
        Task<T> GetById(string entityId);
        Task<T> GetBy(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetPageAsync(int pageNumber, int pageSize, string searchTerm, string filter);
        Task AddAsync(T entity);
        Task UpdateAsync(T Entity);
        Task DeleteAsync(string entityId);
        int GetCount();

    }
}
