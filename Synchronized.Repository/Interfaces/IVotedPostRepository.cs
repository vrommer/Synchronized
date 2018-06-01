using Synchronized.Domain;

namespace Synchronized.Repository.Interfaces
{
    public interface IVotedPostRepository<TEntity> : IPostsRepository<TEntity> where TEntity: VotedPost
    {
    }
}
