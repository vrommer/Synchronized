using Synchronized.ServiceModel.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.ServiceModel
{
    /// <summary>
    /// This class represents a Question on the Business Layer. The Question is also a QuestionPublisher.
    /// </summary>
    public class Question: VotedPost, IQuestionPublisher
    {
        public string Title { get; set; }
        public string Tags { get; set; }
        public int Views { get; set; }
        public ICollection<string> ViewerIds{ get; set; }
        public bool IsAnswered { get; set; }
        public List<IQuestionSubscriber> Subscribers { get; set; }
        public ICollection<Answer> Answers { get; set; }

        public async Task Notify()
        {
            foreach (IQuestionSubscriber s in Subscribers)
            {
                await s.Update();
            }
        }

        public void UnSubscribe(IQuestionSubscriber s)
        {
            throw new System.NotImplementedException();
        }

        public void Subscribe(IQuestionSubscriber sub)
        {
            HashSet<string> subscriberIds = new HashSet<string>()
            {
            };
            foreach (IQuestionSubscriber s in Subscribers)
            {
                subscriberIds.Add(s.Id);
            }
            if (!subscriberIds.Contains(sub.Id))
            {
                sub.NewSubscriber = true;
                Subscribers.Add(sub);
            }
        }
    }
}
