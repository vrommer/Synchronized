using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.Repository.Interfaces
{
    public interface IDataContext : IDisposable
    {
        IQuestionsRepository QuestionsRepository { get; set; }
        IQuestionsRepository LabelsRepository { get; set; }
        IQuestionsRepository QuestionsRepository { get; set; }


    }
}
