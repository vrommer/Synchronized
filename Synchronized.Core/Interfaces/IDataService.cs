using Synchronized.SharedLib.Utilities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IDataService<T> where T: class
    {
        Task<T> GetBy(Expression<Func<T, bool>> predicate);
        Task<T> GetById(string Id);
        Task<PaginatedList<T>> GetPage(int pageNumber, int pageSize, string sortOrder, string filter);
    }
}
