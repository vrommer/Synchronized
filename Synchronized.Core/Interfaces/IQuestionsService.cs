using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;
using SharedLib.Infrastructure.Constants;
using Synchronized.ViewModel;
using Synchronized.ServiceModel;

namespace Synchronized.Core.Interfaces
{
    public interface IQuestionsService : IPostsService<Question>
    {
        Task<PaginatedList<Question>> GetPage(int pageNumber, int pageSize);
        Task<Question> VoteForQuestion(string postId, VoteType upVote, string userId, int userPoints);
        Task<Answer> VoteForAnswer(string postId, VoteType downVote, string userId, int userPoints);
        Task<Question> ViewQuestion(string questionId, string userId);
        Task<string> AskQuestion(Question question);
        Task<bool> TagsAreValid(string tags);
        Task AnswerQuestion(Answer answer, string questionId);
        Task AcceptAnswer(AnswerViewModel answer, string userId);
    }
}
