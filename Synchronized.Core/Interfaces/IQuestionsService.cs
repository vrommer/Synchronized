using Synchronized.ServiceModel;
using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;
using SharedLib.Infrastructure.Constants;
using Synchronized.Domain;

namespace Synchronized.Core.Interfaces
{
    public interface IQuestionsService : IPostsService<ServiceModel.Question>
    {
        Task<PaginatedList<ServiceModel.Question>> GetPage(int pageNumber, int pageSize);
        Task<ServiceModel.Question> VoteForQuestion(string postId, VoteType upVote, string userId);
        Task<ServiceModel.Answer> VoteForAnswer(string postId, VoteType downVote, string id);
    }
}
