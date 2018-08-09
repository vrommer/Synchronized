using Synchronized.Core.Interfaces;
using Synchronized.Domain;
using Synchronized.Repository.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;
using Synchronized.Core.Utilities.Interfaces;
using Synchronized.Core.Factories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Synchronized.Core
{
    /// <summary>
    /// This is a generic DataService. 
    /// </summary>
    /// <typeparam name="TEntity">User defined class</typeparam>
    /// <typeparam name="TServiceModel">User defined class</typeparam>
    public class DataService<TEntity, TServiceModel> : IDataService<TServiceModel>
        where TEntity : class, IEntity
        where TServiceModel : class
    {
        public DataService(IDataRepository<TEntity> repo, IServiceModelFactory factory, IDataConverter converter, ILogger<Object> logger)
        {
            _repo = repo;
            _factory = factory;
            _converter = converter;
            _logger = logger;
        }

        protected IDataRepository<TEntity> _repo;
        protected IServiceModelFactory _factory;
        protected IDataConverter _converter;
        protected ILogger<object> _logger;

        public virtual Task<TServiceModel> GetBy(Expression<Func<TServiceModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TServiceModel> GetById(string Id)
        {

            throw new NotImplementedException();
        }

        public virtual Task<PaginatedList<TServiceModel>> GetPage(int pageIndex, int pageSize, string sortOrder, string searchTerm)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Update(TServiceModel entity)
        {            
            throw new NotImplementedException();
        }
    }
}
