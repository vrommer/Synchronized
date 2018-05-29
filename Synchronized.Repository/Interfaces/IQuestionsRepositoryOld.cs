using Synchronized.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface IQuestionsRepositoryOld : IPostsRepositoryOld<Question>
    {
        //---------------------Should Be Removed------------------------

        Task<List<Question>> GetQuestionsPageAsync(int pageIndex, int pageSize);
        List<Question> GetQuestionsPageWithUsers(int pageIndex, int pageSize);
        List<Question> GetQuestionsPageWithUsers(int pageIndex, int pageSize, string sortOrder, string filter);
        Question FindQuestionById(string questionId);
        Answer FindAnswerById(string answerId);
        void UpdateQuestion(Question question);
        void UpdateAnswer(Answer answer);

        //---------------------Used Methods------------------------
    }
}
