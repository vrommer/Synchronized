using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    /// <summary>
    /// This interface Is for working with Tags in the context of Business Logic
    /// </summary>
    public interface ITagsService: IDataService<Tag>
    {
        /// <summary>
        /// This method is for retreiving a page of tags.
        /// </summary>
        /// <param name="pageIndex">The index of the page</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="sortOrder">The order in which the data should come.</param>
        /// <param name="searchTerm">A search term.</param>
        /// <returns>A list of tags.</returns>
        PaginatedList<Tag> GetTagsPage(int pageIndex, int pageSize, string sortOrder, string searchTerm);

        /// <summary>
        /// This method is for getting all of the Tags. The possible amount of tags makes it physable and sincable to retreive all of the tags.
        /// </summary>
        /// <returns>A list of all of the tags.</returns>
        Task<List<Tag>> GetAllTags();

        /// <summary>
        /// This is a method for creating Tags.
        /// </summary>
        /// <param name="tag">The new tag for creation.</param>
        /// <param name="userPoints">The number of points user has.</param>
        /// <returns></returns>
        Task<string> CreateTag(Tag tag, int userPoints);
    }
}
