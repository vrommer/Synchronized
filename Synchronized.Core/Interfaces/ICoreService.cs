using SharedLib.Infrastructure.Constants;
using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface ICoreService
    {
        Task<Question> GetQuestion(string id);
        Task<List<Question>> GetQuestionsPageAsync(int pageNumber, int pageSize, string searchTerm, string sortOrder);
        Task<List<Question>> GetQuestionsPageAsync(int pageNumber, int pageSize);        
        Task VoteForPostAsync(string postId, VoteType voteType, string userId);
        Task FlagPostAsync(string postId, string userId);
        Task DeletePostAsync(string postId, string userId);
        Task CommentOnPostAsync(string postId, string comment, string userId);
    }
}
