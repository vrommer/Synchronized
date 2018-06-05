using Synchronized.Domain;
using Synchronized.SharedLib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface IQuestionsRepository: IPostsRepository<Question>
    {
        Task<List<Question>> GetPageAsync(int pageNumber, int pageSize);
        Task<Question> GetQuestionById(string id);
        Task<Answer> GetAnswerById(string postId);
        Task<Comment> GetCommentById(string commentId);
        Task UpdateAnswerAsync(Answer answer);
        Task<Tag> GetQuestionTagById(string tagId);
    }
}
