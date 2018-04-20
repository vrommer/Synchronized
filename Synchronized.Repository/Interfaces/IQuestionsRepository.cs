using Synchronized.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface IQuestionsRepository : IDataRepository<Question>
    {
        Task<List<Question>> GetQuestionsPageAsync(int pageIndex, int pageSize);
        List<Question> GetQuestionsPageWithUsersAsync(int pageIndex, int pageSize);
        List<Question> GetQuestionsPageWithUsersAsync(int pageIndex, int pageSize, string sortOrder);
        Question FindQuestionById(string questionId);
    }
}
