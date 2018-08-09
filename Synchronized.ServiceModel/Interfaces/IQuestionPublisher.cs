using System.Threading.Tasks;

namespace Synchronized.ServiceModel.Interfaces
{
    /// <summary>
    /// The question Publisher is responsible for Publishing any change that occurs on the Question.
    /// </summary>
    public interface IQuestionPublisher
    {
        /// <summary>
        /// Subscribe a new Subscriber to the Subscribers List.
        /// </summary>
        /// <param name="s">The new Subscriber.</param>
        void Subscribe(IQuestionSubscriber s);

        /// <summary>
        /// Unsubscriber an existing Subscriber from the Subscribers List.
        /// </summary>
        /// <param name="s">The Subscriber to be Unsubscribed</param>
        void UnSubscribe(IQuestionSubscriber s);

        /// <summary>
        /// Notify all Subscribers about a change in the State of the Model.
        /// </summary>
        Task Notify();
    }
}
