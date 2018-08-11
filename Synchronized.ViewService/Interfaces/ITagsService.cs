using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel.TagsViewModels;

namespace Synchronized.ViewServices.Interfaces
{
    /// <summary>
    /// An interface for working with Tags in the ViewModel Context.
    /// </summary>
    public interface ITagsService
    {
        /// <summary>
        /// A method for retreiving PaginatedList of TagViewModel.
        /// </summary>
        /// <param name="currentPage">The number of the required Page.</param>
        /// <param name="searchTerm">A search term.</param>
        /// <returns>A new instance of PaginatedList of TagViewModel.</returns>
        PaginatedList<TagViewModel> GetIndexPage(int currentPage, string searchTerm);
    }
}
