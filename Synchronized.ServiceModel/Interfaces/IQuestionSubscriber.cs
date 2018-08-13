using System.Threading.Tasks;

namespace Synchronized.ServiceModel.Interfaces
{
    /// <summary>
    /// This interface represents a QuestionSubscriber.
    /// </summary>
    public interface IQuestionSubscriber
    {
        /// <summary>
        /// The email of the Subscriber.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// The Id of the Subscriber.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// An indicator that indicates whether this subscriber is new.
        /// </summary>
        bool NewSubscriber { get; set; }

        /// <summary>
        /// Update the subscriber about change in the state of a Question.
        /// </summary>
        /// <returns></returns>
        Task Update();
    }
}
