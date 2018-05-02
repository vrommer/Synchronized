using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IFlagQuestions
    {
        Task FlagQuestion(string UserId, string questionId);
    }
}
