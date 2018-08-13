using Synchronized.Domain.Factories.Interfaces;
using System;
using System.Collections.Generic;

namespace Synchronized.Domain.Factories
{
    /// <summary>
    /// A concrete Factory for the Domain Model.
    /// </summary>
    public class DomainModelFactory : IDomainModelFactory
    {
        public T GetOfType<T>()
        {
            object obj = Activator.CreateInstance(typeof(T));
            return ((T)obj);
        }

        public Answer GetAnswer()
        {
            return new Answer();
        }

        public Comment GetComment()
        {
            return new Comment();
        }

        public Question GetQuestion()
        {
            var question = new Question
            {
                QuestionTags = new List<QuestionTag>(),
                Subscriptions = new List<Subscription>()
            };
            return question;
        }

        public List<Question> GetQuestionsList()
        {
            return new List<Question>();
        }

        public QuestionTag GetQuestionTag()
        {
            return new QuestionTag();
        }
    }
}
