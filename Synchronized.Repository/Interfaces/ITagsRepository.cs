using Synchronized.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface ITagsRepository : IDataRepository<Tag>
    {
        Task<List<Tag>> GetAllTags();
    }
}
