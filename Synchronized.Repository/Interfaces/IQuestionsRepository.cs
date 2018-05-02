using Synchronized.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface IQuestionsRepository : IPostsRepository<Question>
    {
        Task<List<Question>> GetQuestionsPageAsync(int pageIndex, int pageSize);
        List<Question> GetQuestionsPageWithUsersAsync(int pageIndex, int pageSize);
        List<Question> GetQuestionsPageWithUsersAsync(int pageIndex, int pageSize, string sortOrder, string filter);
        Question FindQuestionById(string questionId);
        Answer FindAnswerById(string answerId);
        void UpdateQuestion(Question question);
        void UpdateAnswer(Answer answer);
    }
}
