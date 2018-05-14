using Synchronized.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IDataService<TServiceModel, TDomainModel> 
        where TServiceModel : class 
        where TDomainModel: class
    {
        IEnumerable<TDomainModel> GetAll();
        Task CreateAsync(TDomainModel entity);
        TDomainModel FindById(string itemId);
        void Add(TDomainModel item);
        void Delete(string itemId);
        void Update(TDomainModel item);
        IEnumerable<TDomainModel> FindBy(Expression<Func<TDomainModel, bool>> predicate);
    }
}
