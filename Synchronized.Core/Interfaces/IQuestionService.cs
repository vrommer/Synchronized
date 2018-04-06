using Synchronized.Model;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IQuestionService : IDataService<Question>
    {
        Task<PaginatedList<Question>> GetQuestionsPage(int pageIndex, int pageSize);
    }
}
