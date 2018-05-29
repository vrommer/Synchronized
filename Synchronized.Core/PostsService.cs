using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Synchronized.Core.Interfaces;
using Synchronized.Repository.Interfaces;
using Synchronized.Core.Utilities.Interfaces;
using Synchronized.Core.Factories.Interfaces;

namespace Synchronized.Core
{
    public class PostsService<TEntity, TServiceModel> : DataService<TEntity, TServiceModel>, IPostsService<TServiceModel> 
        where TServiceModel : ServiceModel.Post
        where TEntity : Domain.Post
    {

        public PostsService(IDataRepository<TEntity> repo, IServiceModelFactory factory, IDataConverter converter)  : base(repo, factory, converter) 
        {
        }

        public Task<ServiceModel.VotedPost> GetVotedPostBy(Expression<Func<ServiceModel.VotedPost, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
