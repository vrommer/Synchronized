using SharedLib.Infrastructure.Constants;
using Synchronized.Core.Utilities.Interfaces;
using System.Linq;
using System.Text;
using Synchronized.Domain;
using Synchronized.ServiceModel;
using Synchronized.Core.Factories.Interfaces;
using System.Collections.Generic;
using System;
using Synchronized.Domain.Factories.Interfaces;

namespace Synchronized.Core.Utilities
{
    public class CoreDataConverter : IDataConverter
    {
        private IServiceModelFactory _serviceModelFactory;
        private IDomainModelFactory _domainModelFactory;

        public CoreDataConverter(IServiceModelFactory serviceModelFactory, IDomainModelFactory domainModel)
        {
            _serviceModelFactory = serviceModelFactory;
            _domainModelFactory = domainModel;
        }

        public List<ServiceModel.Post> Convert(ICollection<Domain.Post> source)
        {
            throw new NotImplementedException();
        }

        public List<ServiceModel.VotedPost> Convert(ICollection<Domain.VotedPost> source)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Convert a list of Domain.Question to ServiceModel.Question.
        /// Recursively converts all Answers and Comments of the Question. 
        /// </summary>
        /// <param name="source">List of Domain.Question</param>
        /// <returns>List of ServiceModel.Question</returns>
        public List<ServiceModel.Question> Convert(ICollection<Domain.Question> source)
        {
            var questions = _serviceModelFactory.GetQuestionsList();
            if (source != null)
            {
                source.ToList().ForEach(q =>
                {
                    questions.Add(Convert(q));
                });
            }
            return questions;
        }

        public List<ServiceModel.Answer> Convert(ICollection<Domain.Answer> source)
        {
            var answers = _serviceModelFactory.GetAnswersList();
            if (source != null)
            {
                source.ToList().ForEach(a =>
                {
                    answers.Add(Convert(a));
                });
            }
            return answers;
        }

        public List<ServiceModel.Comment> Convert(ICollection<Domain.Comment> source)
        {
            var comments = _serviceModelFactory.GetCommentsList();
            if (source != null)
            {
                source.ToList().ForEach(c =>
                {
                    comments.Add(Convert(c));
                });
            }
            return comments;
        }

        public List<User> Convert(ICollection<ApplicationUser> source)
        {
            var users = _serviceModelFactory.GetUsersList();
            foreach (var u in source)
            {
                users.Add(Convert(u));
            }
            return users;
        }

        public List<ServiceModel.PostFlag> Convert(ICollection<Domain.PostFlag> source)
        {
            throw new NotImplementedException();
        }

        public List<PostDelete> Convert(ICollection<DeleteVote> source)
        {
            throw new NotImplementedException();
        }

        public ServiceModel.Post Convert(Domain.Post from)
        {
            throw new NotImplementedException();
        }

        public ServiceModel.VotedPost Convert(Domain.VotedPost from)
        {
            var post = _serviceModelFactory.GetVotedPost();
            post.Body = String.Copy(from.Body);
            if (from.Comments != null)
            {
                post.Comments = Convert(from.Comments.ToList());
            }
            if (from.DeleteVotes != null)
            {
                from.DeleteVotes.ToList().ForEach(v => {
                    post.DeleterIds.Add(v.UserId);
                });
            }
            if (from.PostFlags != null)
            {
                from.PostFlags.ToList().ForEach(f => {
                    post.FlaggerIds.Add(f.UserId);
                });
            }
            if (from.Votes != null)
            {
                from.Votes.ToList().ForEach(f => {
                    post.VoterIds.Add(f.VoterId);
                });
            }
            post.DatePosted = from.DatePosted;
            post.DownVotes = from.Votes.Where(v => v.VoteType == (int)VoteType.DownVote).Count();
            post.UpVotes = from.Votes.Where(v => v.VoteType == (int)VoteType.UpVote).Count();

            return post;
        }

        public ServiceModel.Question Convert(Domain.Question from)
        {

            var builder = new StringBuilder();
            bool first = true;

            var to = _serviceModelFactory.GetQuestion();

            to.Answers = Convert(from.Answers);                      

            to.IsAnswered = from.Answered();
            to.Title = String.Copy(from.Title);

            if (from.QuestionViews != null)
            {
                to.Views = from.QuestionViews.Count();
                foreach (var v in from.QuestionViews)
                {
                    to.ViewerIds.Add(v.UserId);
                }               
            }
            // Add question tags as string
            if (from.QuestionTags != null)
            {
                // Add viewer Ids
                foreach (var questionTag in from.QuestionTags)
                {
                    if (!first)
                    {
                        builder.Append(",").Append(questionTag.Tag.Id);
                    }
                    else
                    {
                        builder.Append(questionTag.Tag.Id);
                        first = false;
                    }
                }
            }
            to.Tags = builder.ToString();
            
            // ServiceModel.Post 
            AddPostPart(from, to);
            // ServiceModel.VotedPost 
            AddVotedPost(from, to);

            return to;
        }

        public ServiceModel.Answer Convert(Domain.Answer from)
        {
            var to = _serviceModelFactory.GetAnswer();
            to.IsAccepted = from.IsAccepted;
            to.QuestionId = String.Copy(from.QuestionId);
            // ServiceModel.Post
            AddPostPart(from, to);
            // ServiceModel.VotedPost
            AddVotedPost(from, to);
            return to;
        }

        public ServiceModel.Comment Convert(Domain.Comment from)
        {
            var to = _serviceModelFactory.GetComment();
            to.VotedPostId = String.Copy(from.PostId);
            // ServiceModel.Post 
            AddPostPart(from, to);

            return to;
        }

        public User Convert(ApplicationUser from)
        {
            var serviceUser = _serviceModelFactory.GetUser();
            serviceUser.Id = String.Copy(from.Id);
            serviceUser.Name = String.Copy(from.UserName);
            serviceUser.Email = String.Copy(from.Email);
            serviceUser.Address = from.Address != null ? String.Copy(from.Address): "";
            serviceUser.ImageUri = from.ImageUri != null ? String.Copy(from.ImageUri): "";
            return serviceUser;
        }

        public ServiceModel.PostFlag Convert(Domain.PostFlag from)
        {
            throw new NotImplementedException();
        }

        public PostDelete Convert(DeleteVote from)
        {
            throw new NotImplementedException();
        }

        public Domain.Post Convert(ServiceModel.Post from)
        {
            throw new NotImplementedException();
        }

        public Domain.VotedPost Convert(ServiceModel.VotedPost from)
        {
            throw new NotImplementedException();
        }

        public Domain.Question Convert(ServiceModel.Question from)
        {
            var question = _domainModelFactory.GetQuestion();
            question.Title = String.Copy(from.Title);
            question.Body = String.Copy(from.Body);
            question.PublisherId = String.Copy(from.PublisherId);
            var tageNamesArray = from.Tags.Split(',');
            for (int i = 0; i<tageNamesArray.Length; i++)
            {
                var questionTag = _domainModelFactory.GetQuestionTag();
                questionTag.TagId = String.Copy(tageNamesArray[i]);
                question.QuestionTags.Add(questionTag);
            }
            return question;
        }

        public Domain.Answer Convert(ServiceModel.Answer from)
        {
            var answer = _domainModelFactory.GetAnswer();
            answer.PublisherId = String.Copy(from.PublisherId);
            answer.Body = String.Copy(from.Body);
            return answer;
        }

        public Domain.Comment Convert(ServiceModel.Comment from)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Convert(User from)
        {
            throw new NotImplementedException();
        }

        public Domain.PostFlag Convert(ServiceModel.PostFlag from)
        {
            throw new NotImplementedException();
        }

        public DeleteVote Convert(PostDelete from)
        {
            throw new NotImplementedException();
        }

        public List<Domain.Post> Convert(ICollection<ServiceModel.Post> from)
        {
            throw new NotImplementedException();
        }

        public List<Domain.VotedPost> Convert(ICollection<ServiceModel.VotedPost> from)
        {
            throw new NotImplementedException();
        }

        public List<Domain.Question> Convert(ICollection<ServiceModel.Question> from)
        {
            throw new NotImplementedException();
        }

        public List<Domain.Answer> Convert(ICollection<ServiceModel.Answer> from)
        {
            throw new NotImplementedException();
        }

        public List<Domain.Comment> Convert(ICollection<ServiceModel.Comment> from)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> Convert(ICollection<User> from)
        {
            throw new NotImplementedException();
        }

        public List<Domain.PostFlag> Convert(ICollection<ServiceModel.PostFlag> from)
        {
            throw new NotImplementedException();
        }

        public List<DeleteVote> Convert(ICollection<PostDelete> from)
        {
            throw new NotImplementedException();
        }

        public ServiceModel.Tag Convert(Domain.Tag from)
        {
            var serviceTag =_serviceModelFactory.GetTag();
            serviceTag.Description = String.Copy(from.Description);
            serviceTag.Name = String.Copy(from.Id);
            return serviceTag;
        }

        public Domain.Tag Convert(ServiceModel.Tag from)
        {
            throw new NotImplementedException();
        }

        public List<ServiceModel.Tag> Convert(ICollection<Domain.Tag> from)
        {
            var tags = _serviceModelFactory.GetTagsList();
            foreach (var t in from)
            {
                tags.Add(Convert(t));
            }
            return tags;
        }

        public List<Domain.Tag> Convert(ICollection<ServiceModel.Tag> from)
        {
            throw new NotImplementedException();
        }

        public List<QuestionTag> Convert(string tags)
        {
            var questionTags = _domainModelFactory.GetInstance<List<QuestionTag>>(); 
            var tageNamesArray = tags.Split(',');
            for (int i = 0; i < tageNamesArray.Length; i++)
            {
                var questionTag = _domainModelFactory.GetQuestionTag();
                questionTag.TagId = String.Copy(tageNamesArray[i]);
                questionTags.Add(questionTag);
            }
            return questionTags;
        }

        public string Convert(List<QuestionTag> tags)
        {
            throw new NotImplementedException();
        }

        private void AddPostPart(Domain.Post from, ServiceModel.Post to)
        {
            to.Id = String.Copy(from.Id);
            to.Body = String.Copy(from.Body);
            to.DatePosted = from.DatePosted;
            if (from.Publisher != null)
            {
                to.PublisherName = from.Publisher.UserName;
            }
            to.PublisherId = String.Copy(from.PublisherId);
        }

        private void AddVotedPost(Domain.VotedPost from, ServiceModel.VotedPost to)
        {
            to.Comments = Convert(from.Comments);
            // Add voter Ids
            if (from.Votes != null)
            {
                to.UpVotes = from.Votes.Where(v => v.VoteType == (int)VoteType.UpVote).Count();
                to.DownVotes = from.Votes.Where(v => v.VoteType == (int)VoteType.DownVote).Count();
                from.Votes.ToList().ForEach(v =>
                {
                    to.VoterIds.Add(v.VoterId);
                });
            }
            // Add flagger Ids
            if (from.PostFlags != null)
            {
                foreach (var flagger in from.PostFlags)
                {
                    to.FlaggerIds.Add(flagger.UserId);
                }
            }
        }
    }
}
