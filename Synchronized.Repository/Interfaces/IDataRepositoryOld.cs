using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface IDataRepositoryOld<TModel> where TModel : class
    {
        //-----------------------ShouldBeRemoved----------------------        
        // void Add(TModel item);        
        //------------------------ UsedMethods ------------------------
        //Task AddAsync(TModel entity);
        //Task UpdateAsync(TModel item);
        //Task DeleteAsync(string itemId);
        //Task<TModel> GetByIdAsync(string itemId);
        //Task<IQueryable<TModel>> GetByAsync(Expression<Func<TModel, bool>> predicate);        
        //Task<int> GetCountAsync();
        //Task<IQueryable<TModel>> GetPageAsync(int pageIndex, int pageSize);
    }
}
