using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel.TagsViewModels;

namespace Synchronized.ViewServices.Interfaces
{
    public interface ITagsService
    {
        PaginatedList<TagViewModel> GetIndexPage(int currentPage, string searchTerm);
    }
}
