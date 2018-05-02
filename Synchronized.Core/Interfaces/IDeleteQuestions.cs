using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IDeleteQuestions
    {
        Task DeleteQuestion(string userId, string questionId);
    }
}
