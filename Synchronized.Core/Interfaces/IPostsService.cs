using Synchronized.ServiceModel;

namespace Synchronized.Core.Interfaces
{
    public interface IPostsService<TEntity>: IDataService<TEntity> where TEntity: Post
    {

    }
}
