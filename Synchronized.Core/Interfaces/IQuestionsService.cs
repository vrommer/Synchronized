using Synchronized.Model;
using Synchronized.SharedLib.Utilities;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IQuestionsService : IDataService<Question>
    {
        Task<PaginatedList<Question>> GetQuestionsPageAsync(int pageIndex, int pageSize);
        Task<PaginatedList<Question>> GetQuestionsPageWithUsersAsync(int pageIndex, int pageSize);
        Task<PaginatedList<Question>> GetQuestionsPageWithUsersAsync(int pageIndex, int pageSize, string sortOrder);
        Question FindQuestionById(string questionId);
    }
}
