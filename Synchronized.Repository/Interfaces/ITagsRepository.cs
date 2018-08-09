using Synchronized.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    /// <summary>
    /// This interface defines a repository for working with Tags.
    /// </summary>
    public interface ITagsRepository : IDataRepository<Tag>
    {
        /// <summary>
        /// Retreives a list of all the Tags from the Database.
        /// </summary>
        /// <returns>A List of all the tags in the Database.</returns>
        Task<List<Tag>> GetAllTags();
    }
}
