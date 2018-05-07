using Synchronized.Core.Interfaces;
using Synchronized.Model;
using Synchronized.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Core
{
    public class DataService<TEntity> : IDataService<TEntity> where TEntity : class, IEntity
    {
        protected readonly IDataRepository<TEntity> _repo;

        public DataService(IDataRepository<TEntity> repo)
        {
            this._repo = repo;
        }

        public void Add(TEntity item)
        {
            _repo.Add(item);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _repo.AddAsync(entity);
        }

        public void Delete(string itemId)
        {
            _repo.Delete(itemId);
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _repo.FindBy(predicate);
        }

        public TEntity FindById(string itemId)
        {
            return _repo.FindById(itemId);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repo.GetAll();
        }

        public void Update(TEntity item)
        {
            _repo.Update(item);
        }
    }
}
