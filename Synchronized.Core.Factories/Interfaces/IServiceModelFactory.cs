using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;
using System.Collections.Generic;

namespace Synchronized.Core.Factories.Interfaces
{
    public interface IServiceModelFactory
    {
        Question GetQuestion();
        Answer GetAnswer();
        Comment GetComment();
        PaginatedList<Question> GetQuestionsList(int totalSize, int pageIndex, int pageSize);
        PaginatedList<Question> GetQuestionsList(List<Question> questions, int totalSize, int pageIndex, int pageSize);
        List<Question> GetQuestionsList();
        User GetUser();
        List<Answer> GetAnswersList();
        List<Comment> GetCommentsList();
        VotedPost GetVotedPost();
    }
}
