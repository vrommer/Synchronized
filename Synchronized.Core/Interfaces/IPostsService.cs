using Synchronized.ServiceModel;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IPostsService<TEntity>: IDataService<TEntity> where TEntity: Post
    {
        Task<bool> DeletePost(string postId, string userId, int userPoints);
    }
}
