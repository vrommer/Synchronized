using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Synchronized.Repository.Interfaces
{
    public interface IDataRepository<TModel> : IDisposable where TModel : class
    {
        IEnumerable<TModel> GetAll();
        TModel FindById(string itemId);
        void Add(TModel item);
        void Delete(string itemId);
        void Update(TModel item);
        IEnumerable<TModel> FindBy(Expression<Func<TModel, bool>> predicate);
    }
}
