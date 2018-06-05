using Synchronized.Domain.Factories.Interfaces;
using System.Collections.Generic;

namespace Synchronized.Domain.Factories
{
    public class DomainModelFactory : IDomainModelFactory
    {
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
                QuestionTags = new List<QuestionTag>()
            };
            return question;
        }

        public QuestionTag GetQuestionTag()
        {
            return new QuestionTag();
        }
    }
}
