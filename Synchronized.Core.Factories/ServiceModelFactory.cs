using Synchronized.ServiceModel;
using System.Collections.Generic;
using Synchronized.SharedLib.Utilities;
using Synchronized.Core.Factories.Interfaces;

namespace Synchronized.Core.Factories
{
    public class ServiceModelFactory : IServiceModelFactory
    {
        public Question GetQuestion()
        {
            var question = new Question
            {
                // Post
                FlaggerIds = new List<string>(),
                // VotedPost
                Comments = new List<Comment>(),
                VoterIds = new List<string>(),
                // Question
                Answers = new List<Answer>(),
                ViewerIds = new List<string>()
            };

            return question;
        }

        public Answer GetAnswer()
        {
            var answer = new Answer
            {
                // Post
                FlaggerIds = new List<string>(),
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
                FlaggerIds = new List<string>(),
            };

            return comment;
        }

        public PaginatedList<Question> GetQuestionsList(int totalSize, int pageIndex, int pageSize)
        {
            return new PaginatedList<Question>(totalSize, pageIndex, pageSize);
        }

        public PaginatedList<Question> GetQuestionsList(List<Question> questions, int count, int pageIndex, int pageSize)
        {
            return new PaginatedList<Question>(questions, count, pageIndex, pageSize);
        }

        public User GetUser()
        {
            return new User();
        }

        public List<Question> GetQuestionsList()
        {
            return new List<Question>();
        }

        public List<Answer> GetAnswersList()
        {
            return new List<Answer>();
        }

        List<Comment> IServiceModelFactory.GetCommentsList()
        {
            return new List<Comment>();
        }

        public VotedPost GetVotedPost()
        {
            return new VotedPost
            {
                VoterIds = new List<string>(),
                DeleterIds = new List<string>(),
                FlaggerIds = new List<string>()                 
            };
        }
    }
}
