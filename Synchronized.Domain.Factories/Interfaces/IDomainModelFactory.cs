using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.Domain.Factories.Interfaces
{
    public interface IDomainModelFactory
    {
        Question GetQuestion();
        Answer GetAnswer();
        Comment GetComment();
        QuestionTag GetQuestionTag();
    }
}
