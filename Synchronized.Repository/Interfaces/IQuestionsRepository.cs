using Synchronized.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface IQuestionsRepository : IDataRepository<Question>
    {
        Task<List<Question>> GetQuestionsPageWithTagsAsync(int pageIndex, int pageSize);
    }
}
