using System;
using Xunit;

namespace Synchronized.Core.Tests
{
    public class TestFactories
    {
        [Fact]
        public void TestGetQuestion()
        {
            var factory = TestUtils.GetModelFactory();
            var domainQuestion = TestUtils.GetQuestionDomainModel();
            var questionServiceModel = factory.GetQuestion(domainQuestion);
        }

        [Fact]
        public void TestGetAnswer()
        {
            var factory = TestUtils.GetModelFactory();
            var question = TestUtils.GetQuestionDomainModel();
            var answerServiceModel = factory.GetAnswer(TestUtils.GetAnswerDomainModel("testQuestionId", question));
        }

        [Fact]
        public void TestGetComment()
        {
            var factory = TestUtils.GetModelFactory();
            var comment = TestUtils.GetCommentDomainMode();
            var commentsServiceModel = factory.GetComment(comment);
        }

    }
}
