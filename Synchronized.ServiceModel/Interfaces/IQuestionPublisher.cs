using Synchronized.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.ServiceModel.Interfaces
{
    public interface IQuestionPublisher
    {
        void Subscribe(IQuestionSubscriber s);
        void UnSubscribe(IQuestionSubscriber s);
        Task Notify();
    }
}
