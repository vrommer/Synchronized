﻿using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IQuestionsServiceOld : IDataServiceOld<Question, Domain.Question>
    {
        Task<PaginatedList<Question>> GetQuestionsPageAsync(int pageIndex, int pageSize);
        Task<PaginatedList<Question>> GetQuestionsPageWithUsersAsync(int pageIndex, int pageSize);
        Task<PaginatedList<Question>> GetQuestionsIndexPage(int pageIndex, int pageSize, string sortOrder, string filter);
        Question FindQuestionById(string questionId);
        Answer FindAnswerById(string answerId);
        void UpdateQuestion(Question question);
        void UpdateAnswer(Answer answer);
    }
}
