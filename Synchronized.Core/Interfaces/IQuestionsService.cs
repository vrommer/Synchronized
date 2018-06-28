using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;
using SharedLib.Infrastructure.Constants;
using Synchronized.ViewModel;
using Synchronized.Domain;

namespace Synchronized.Core.Interfaces
{
    public interface IQuestionsService : IPostsService<ServiceModel.Question>
    {
        Task<PaginatedList<ServiceModel.Question>> GetPage(int pageNumber, int pageSize);
        Task<ServiceModel.Question> VoteForQuestion(string postId, VoteType upVote, ApplicationUser User);
        Task<ServiceModel.Answer> VoteForAnswer(string postId, VoteType downVote, ApplicationUser User);
        Task<ServiceModel.Question> ViewQuestion(string questionId, string userId);
        Task<string> AskQuestion(ServiceModel.Question question);
        Task<bool> TagsAreValid(string tags);
        Task AnswerQuestion(ServiceModel.Answer answer, string questionId);
        Task AcceptAnswer(AnswerViewModel answer,ApplicationUser user);
    }
}
