using System.Threading.Tasks;
using Synchronized.Domain;
using Synchronized.SharedLib.Utilities;
using System.Collections.Generic;

namespace Synchronized.Repository.Interfaces
{
    public interface ITagsRepository : IDataRepositoryOld<Tag>
    {
        Task<List<Tag>> GetTagsPageAsync(int currentPage, int pageSize);
        Task<Tag> FindTagByName(string name);
    }
}
