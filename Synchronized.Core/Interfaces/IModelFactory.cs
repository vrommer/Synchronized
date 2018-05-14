using Synchronized.ServiceModel;

namespace Synchronized.Core.Interfaces
{
    public interface IModelFactory
    {
        Question GetQuestion(Model.Question question);
        Answer GetAnswer(Model.Answer answer);
        Comment GetComment(Model.Comment comment);
        PostFlag GetPostFlag(Domain.PostFlag flag);
    }
}
