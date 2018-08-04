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
        PaginatedList<Question> GetQuestionsPage(int totalSize, int pageIndex, int pageSize);
        PaginatedList<Question> GetQuestionsPage(List<Question> questions, int totalSize, int pageIndex, int pageSize);
        List<Question> GetQuestionsList();
        List<Tag> GetTagsList();
        User GetUser();
        List<User> GetUsersList();
        List<Answer> GetAnswersList();
        List<Comment> GetCommentsList();
        VotedPost GetVotedPost();
        Tag GetTag();
        PaginatedList<Tag> GetTagsPage(List<Tag> tags, int count, int pageSize, int pageIndex);
        T GetOfType<T>();
        PaginatedList<User> GetUsersPage(List<User> users, int count, int pageSize, int pageIndex);
    
    }
}
