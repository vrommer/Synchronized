using Synchronized.Domain;

namespace Synchronized.Repository.Interfaces
{
    public interface IPostsRepository<TModel>: IDataRepository<TModel> where TModel: Post
    {
    }
}
