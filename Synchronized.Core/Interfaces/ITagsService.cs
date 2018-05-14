using System.Threading.Tasks;
using Synchronized.Model;
using Synchronized.SharedLib.Utilities;

namespace Synchronized.Core.Interfaces
{
    public interface ITagsService: IDataService<ServiceModel.Post, Tag>
    {
        Task<PaginatedList<Tag>> GetTagsPageAsync(int currentPage, int pageSize);

        Task<Tag> FindTagByName(string name);
    }
}