using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IServiceModelFactory
    {
        Question GetQuestion();
        Answer GetAnswer();
        Comment GetComment();
        PaginatedList<Question> GetQuestionsList(int totalSize, int pageIndex, int pageSize);
        PaginatedList<Question> GetQuestionsList(List<Question> questions, int totalSize, int pageIndex, int pageSize);
    }
}
