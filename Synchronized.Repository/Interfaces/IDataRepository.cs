using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface IDataRepository<TModel> where TModel : class
    {
        IEnumerable<TModel> GetAll();
        IQueryable<TModel> GetPage(int pageIndex, int pageSize);
        Task<int> GetCount();
        Task AddAsync(TModel entity);
        TModel FindById(string itemId);
        void Add(TModel item);
        void Delete(string itemId);
        void Update(TModel item);
        IQueryable<TModel> FindBy(Expression<Func<TModel, bool>> predicate);        
    }
}
