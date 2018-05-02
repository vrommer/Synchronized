using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IViewQuestions
    {
        Task ViewQuestion(string UserId, string questionId);
    }
}
