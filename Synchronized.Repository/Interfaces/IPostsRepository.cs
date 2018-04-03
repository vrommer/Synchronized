using Synchronized.Model;

namespace Synchronized.Repository.Interfaces
{
    public interface IPostsRepository<TPost> : IDataRepository<TPost>  where TPost: Post
    {
    }
}
