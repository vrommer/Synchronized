using Synchronized.ServiceModel;
using System.Collections.Generic;

namespace Synchronized.Core.Factories
{
    public class ServiceModelFactory
    {
        public Question GetQuestion()
        {
            var question = new Question {
                // Post
                Flags = new List<PostFlag>(),
                // VotedPost
                Comments = new List<Comment>(),
                VoterIds = new List<string>(),
                // Question
                Answers = new List<Answer>()
            };

            return question;
        }

        public Answer GetAnswer()
        {
            var answer = new Answer {
                // Post
                Flags = new List<PostFlag>(),
                // VotedPost
                Comments = new List<Comment>(),
                VoterIds = new List<string>()
            };

            return answer;
        }

        public Comment GetComment()
        {
            var comment = new Comment {
                // Post
                Flags = new List<PostFlag>(),
            };

            return comment;
        }
    }
}
