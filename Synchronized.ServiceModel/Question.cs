using Synchronized.ServiceModel.Interfaces;
using Synchronized.SharedLib.Interfaces;
using Synchronized.SharedLib.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.ServiceModel
{
    public class Question: VotedPost, IQuestionPublisher
    {
        public string Title { get; set; }
        public string Tags { get; set; }
        public int Views { get; set; }
        public ICollection<string> ViewerIds{ get; set; }
        public bool IsAnswered { get; set; }
        public List<IQuestionSubscriber> Subscribers { get; set; }

        public static readonly IEmailSender _emailSender = new EmailSender();

        public ICollection<Answer> Answers { get; set; }

        public async Task Notify()
        {
            foreach (IQuestionSubscriber s in Subscribers)
            {
                await _emailSender.SendEmailAsync(s.Email, "subject", "message");
            }
        }

        public void UnSubscribe(IQuestionSubscriber s)
        {
            throw new System.NotImplementedException();
        }

        public void Subscribe(IQuestionSubscriber s)
        {
            throw new System.NotImplementedException();
        }
    }
}
