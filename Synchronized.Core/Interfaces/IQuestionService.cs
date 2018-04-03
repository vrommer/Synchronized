using Synchronized.Model;
using Synchronized.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IQuestionService : IDataService<Question>
    {
        Task<HomeViewModel> GetHomeViewModel(int pageIndex, int pageSize);
    }
}
