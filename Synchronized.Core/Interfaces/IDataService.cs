using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IDataService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        Task CreateAsync(TEntity entity);
        TEntity FindById(string itemId);
        void Add(TEntity item);
        void Delete(string itemId);
        void Update(TEntity item);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
    }
}
