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
    }
}
