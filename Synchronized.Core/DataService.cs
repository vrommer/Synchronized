using Synchronized.Core.Interfaces;
using Synchronized.Model;
using Synchronized.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Core
{
    public class DataService<TEntity, TModel> : IDataService<TEntity, TModel> 
        where TEntity : class
        where TModel: class, IEntity
    {
        protected readonly IDataRepository<TModel> _repo;

        public DataService(IDataRepository<TModel> repo)
        {
            _repo = repo;
        }

        public void Add(TModel item)
        {
            _repo.Add(item);
        }

        public async Task CreateAsync(TModel entity)
        {
            await _repo.AddAsync(entity);
        }

        public void Delete(string itemId)
        {
            _repo.Delete(itemId);
        }

        public IEnumerable<TModel> FindBy(Expression<Func<TModel, bool>> predicate)
        {
            return _repo.FindBy(predicate);
        }

        public TModel FindById(string itemId)
        {
            return _repo.FindById(itemId);
        }

        public IEnumerable<TModel> GetAll()
        {
            return _repo.GetAll();
        }

        public void Update(TModel item)
        {
            _repo.Update(item);
        }
    }
}
