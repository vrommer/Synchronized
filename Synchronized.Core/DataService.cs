using Synchronized.Core.Interfaces;
using Synchronized.Model;
using Synchronized.Repository.Interfaces;
using Synchronized.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Synchronized.Core
{
    public class DataService<TEntity> : IDataService<TEntity> where TEntity : class, IEntity
    {
        protected readonly IDataRepository<TEntity> repo;

        public DataService(IDataRepository<TEntity> repo)
        {
            this.repo = repo;
        }

        public void Add(TEntity item)
        {
            repo.Add(item);
        }

        public void Delete(string itemId)
        {
            repo.Delete(itemId);
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return repo.FindBy(predicate);
        }

        public TEntity FindById(string itemId)
        {
            return repo.FindById(itemId);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return repo.GetAll();
        }

        public void Update(TEntity item)
        {
            repo.Update(item);
        }
    }
}
