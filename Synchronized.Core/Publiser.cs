using Microsoft.Extensions.Logging;
using Synchronized.Core.Factories.Interfaces;
using Synchronized.Core.Interfaces;
using Synchronized.Core.Utilities.Interfaces;
using Synchronized.ServiceModel;
using Synchronized.ServiceModel.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.Core
{
    public class Publisher : IQuestionPublisher
    {
        private ILogger<Publisher> _logger;
        private IServiceModelFactory _factory;
        private IDataConverter _converter;
        private List<IQuestionSubscriber> _subscribers;

        public Publisher(IDataConverter converter, ILogger<Publisher> logger, IServiceModelFactory factory)
        {
            _factory = factory;
            _logger = logger;
            _converter = converter;
        }

        public List<IQuestionSubscriber> Subscribers { get; set; }

        public Publisher(IServiceModelFactory factory, ILogger<Publisher> logger)
        {
            _logger = logger;
            _factory = factory;
            Subscribers = factory.GetOfType<List<IQuestionSubscriber>>();
        }

        public async Task Notify(Question question)
        {
            _logger.LogInformation("Calling notify.");

            foreach (var s in Subscribers)
            {
                var name = ((User)s).Name;
                _logger.LogDebug("Notifying {NAME}", name);
                await s.Update();
            }
            _logger.LogInformation("Finished notify.");
        }

        //public void Subscribe(IQuestionSubscriber s)
        //{
        //    var subscription = _factory.GetOfType<Subscription>();
        //    subscription.UserId = userId;
        //    if (!question.Subscriptions.Contains(subscription))
        //    {
        //        question.Subscriptions.Add(subscription);
        //    }
        //    Subscribers.Add(s);
        //}

        public void UnSubscribe(IQuestionSubscriber s)
        {
            Subscribers.Remove(s);
        }

        public async Task Notify(Domain.Question question)
        {
            _logger.LogInformation("Calling notify.");
            foreach (var s in question.Subscriptions)
            {
                var user = _converter.Convert(s.Subscriber);
                var name = user.Name;
                _logger.LogDebug("Notifying {NAME}", name);
                await user.Update();
            }
            foreach (var s in Subscribers)
            {
                var name = ((User)s).Name;
                _logger.LogDebug("Notifying {NAME}", name);
                await s.Update();
            }
            _logger.LogInformation("Finished notify.");
        }

        public Task Notify()
        {
            throw new System.NotImplementedException();
        }

        public void Subscribe(IQuestionSubscriber s)
        {
            throw new System.NotImplementedException();
        }
    }
}
