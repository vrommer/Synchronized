using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface ITagsService: IDataService<Tag>
    {
        PaginatedList<Tag> GetTagsPage(int pageIndex, int pageSize, string sortOrder, string searchTerm);
        Task<List<Tag>> GetAllTags();
        Task<string> CreateTag(Tag tag, int userPoints);
    }
}
