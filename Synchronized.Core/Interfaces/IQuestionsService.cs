using Synchronized.ServiceModel;
using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;

namespace Synchronized.Core.Interfaces
{
    public interface IQuestionsService : IPostsService<Question>
    {
        Task<PaginatedList<Question>> GetPage(int pageNumber, int pageSize);
    }
}
