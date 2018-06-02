using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Synchronized.Core.Interfaces;
using Synchronized.Repository.Interfaces;
using Synchronized.Core.Utilities.Interfaces;
using Synchronized.Core.Factories.Interfaces;
using SharedLib.Infrastructure.Constants;
using Synchronized.ServiceModel;

namespace Synchronized.Core
{
    public class PostsService<TEntity, TServiceModel> : DataService<TEntity, TServiceModel>, IPostsService<TServiceModel> 
        where TServiceModel : Post
        where TEntity : Domain.Post
    {

        public PostsService(IPostsRepository<TEntity> repo, IServiceModelFactory factory, IDataConverter converter)  : base(repo, factory, converter) 
        {
        }
    }
}
