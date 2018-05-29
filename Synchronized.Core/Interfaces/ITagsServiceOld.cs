using System.Threading.Tasks;
using Synchronized.Domain;
using Synchronized.SharedLib.Utilities;

namespace Synchronized.Core.Interfaces
{
    public interface ITagsService: IDataServiceOld<ServiceModel.Post, Tag>
    {
        Task<PaginatedList<Tag>> GetTagsPageAsync(int currentPage, int pageSize);

        Task<Tag> FindTagByName(string name);
    }
}