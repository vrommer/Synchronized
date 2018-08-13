using Synchronized.ServiceModel;
using System.Collections.Generic;
using Synchronized.SharedLib.Utilities;
using Synchronized.Core.Factories.Interfaces;
using System;

namespace Synchronized.Core.Factories
{
    /// <summary>
    /// A factory for creating items of the ServiceModel.
    /// </summary>
    public class ServiceModelFactory : IServiceModelFactory
    {
        public Question GetQuestion()
        {
            var question = new Question
            {
                // Post
                FlaggerIds = new Dictionary<string, string>(),
                // VotedPost
                Comments = new List<Comment>(),
                VoterIds = new HashSet<string>(),
                // Question
                Answers = new List<Answer>(),
                ViewerIds = new List<string>(),
                UpVotersIds= new HashSet<string>(),
                DownVotersIds = new HashSet<string>()
            };

            return question;
        }

        public Answer GetAnswer()
        {
            var answer = new Answer
            {
                // Post
                FlaggerIds = new Dictionary<string, string>(),
                // VotedPost
                Comments = new List<Comment>(),
                VoterIds = new HashSet<string>(),
                UpVotersIds = new HashSet<string>(),
                DownVotersIds = new HashSet<string>()
            };

            return answer;
        }

        public Comment GetComment()
        {
            var comment = new Comment {
                // Post
                FlaggerIds = new Dictionary<string, string>()
            };

            return comment;
        }

        public PaginatedList<Question> GetQuestionsPage(int totalSize, int pageIndex, int pageSize)
        {
            return new PaginatedList<Question>(totalSize, pageIndex, pageSize);
        }

        public PaginatedList<Question> GetQuestionsPage(List<Question> questions, int count, int pageIndex, int pageSize)
        {
            return new PaginatedList<Question>(questions, count, pageIndex, pageSize);
        }

        public User GetUser()
        {
            var user = new User
            {
                Questions = new List<Question>()
            };
            return user;
        }

        public List<User> GetUsersList()
        {
            return new List<User>();
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
                VoterIds = new HashSet<string>(),
                DeleterIds = new List<string>(),
                FlaggerIds = new Dictionary<string, string>()                 
            };
        }

        public Tag GetTag()
        {
            return new Tag();           
        }

        public List<Tag> GetTagsList()
        {
            return new List<Tag>();
        }

        public PaginatedList<Tag> GetTagsPage(List<Tag> tags, int count, int pageSize, int pageIndex)
        {
            return new PaginatedList<Tag>(tags, count, pageIndex, pageSize);
        }

        public PaginatedList<User> GetUsersPage(List<User> users, int count, int pageSize, int pageIndex)
        {
            return new PaginatedList<User>(users, count, pageIndex, pageIndex);           
        }

        public T GetOfType<T>()
        {
            object obj = Activator.CreateInstance(typeof(T));
            return ((T)obj);
        }
    }
}
