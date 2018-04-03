using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.Repository.Interfaces
{
    public interface IDataRepository<TModel> : IDisposable
    {
        IEnumerable<TModel> GetAll();
        TModel Get(int itemId);
        void Add(TModel item);
        void Delete(int itemId);
        void UpdateItem(TModel item);
    }
}
