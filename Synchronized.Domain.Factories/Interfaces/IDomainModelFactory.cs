using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.Domain.Factories.Interfaces
{
    public interface IDomainModelFactory
    {
        T GetInstance<T>();
        Question GetQuestion();
        List<Question> GetQuestionsList();
        Answer GetAnswer();
        Comment GetComment();
        QuestionTag GetQuestionTag();
    }
}
