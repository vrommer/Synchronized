using Synchronized.Domain;

namespace Synchronized.Repository.Interfaces
{
    /// <summary>
    /// This class represents a Repository for working with VotedPosts.
    /// </summary>
    public interface IVotedPostRepository : IPostsRepository<VotedPost>
    {
    }
}
