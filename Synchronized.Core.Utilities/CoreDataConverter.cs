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
using Microsoft.Extensions.Logging;
using Synchronized.ServiceModel.Interfaces;

namespace Synchronized.Core.Utilities
{
    public class CoreDataConverter : IDataConverter
    {
        private IServiceModelFactory _serviceModelFactory;
        private IDomainModelFactory _domainModelFactory;
        private ILogger<CoreDataConverter> _logger;

        public CoreDataConverter(IServiceModelFactory serviceModelFactory, IDomainModelFactory domainModel, ILogger<CoreDataConverter> logger)
        {
            _serviceModelFactory = serviceModelFactory;
            _domainModelFactory = domainModel;
            _logger = logger;
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
            _logger.LogInformation("Entering Convert.");
            var comments = _serviceModelFactory.GetCommentsList();
            if (source != null)
            {
                source.ToList().ForEach(c =>
                {
                    comments.Add(Convert(c));
                });
            }
            _logger.LogInformation("Leaving Convert.");
            return comments;
        }

        public List<User> Convert(ICollection<ApplicationUser> source)
        {
            _logger.LogInformation("Entering Convert.");
            var users = _serviceModelFactory.GetUsersList();
            foreach (var u in source)
            {
                users.Add(Convert(u));
            }
            _logger.LogInformation("Leaving Convert.");
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
                    post.FlaggerIds.Add(f.Id, f.UserId);
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
            to.Subscribers = _serviceModelFactory.GetOfType<List<IQuestionSubscriber>>();

            to.Answers = Convert(from.Answers);                      

            to.IsAnswered = from.Answered();
            to.Title = String.Copy(from.Title);

            if (from.Subscriptions != null)
            {
                foreach (Domain.Subscription s in from.Subscriptions)
                {
                    var subscriber = _serviceModelFactory.GetUser();
                    subscriber.Id = String.Copy(s.UserId);
                    subscriber.Email = String.Copy(s.Subscriber.Email);
                    to.Subscribers.Add(subscriber);
                }
            }

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
            if (from.Question != null)
            {
                to.QuestionPublisherId = String.Copy(from.Question.PublisherId);
            }
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
            serviceUser.Points = from.Points;
            if (from.Subscriptions != null)
            {
                foreach (Domain.Subscription s in from.Subscriptions)
                {
                    serviceUser.Questions.Add(Convert(s.Question));
                }
            }
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
            Domain.VotedPost domainPost;
            if (from.GetType().Equals(typeof(ServiceModel.Question)))
            {
                domainPost = _domainModelFactory.GetQuestion();
                if (!String.IsNullOrWhiteSpace(((ServiceModel.Question)from).Title))
                {
                    ((Domain.Question)domainPost).Title = String.Copy(((ServiceModel.Question)from).Title);
                }
            }
            else
            {
                domainPost = _domainModelFactory.GetAnswer();
            }
            if (!String.IsNullOrWhiteSpace(from.Body))
            {
                domainPost.Body = String.Copy(from.Body);
            }
            domainPost.Review = from.Review;
            return domainPost;
        }

        public Domain.Question Convert(ServiceModel.Question from)
        {
            // Get new Domain.Question instance
            var question = _domainModelFactory.GetQuestion();
            // *******************
            // Copy Post part
            // *******************
            AddPostPart(from, question);
            // *******************
            // Copy VotedPost part
            // *******************        
            AddVotedPost(from, question);
            // *******************
            // Copy Question part
            // *******************
            //Copy Title
            question.Title = String.Copy(from.Title);            
            if (from.Answers != null)
            {
                question.Answers = _serviceModelFactory.GetOfType<List<Domain.Answer>>();
                foreach (ServiceModel.Answer a in from.Answers)
                {
                    question.Answers.Add(Convert(a));
                }
            }
            // Copy Views
            if (from.ViewerIds != null)
            {
                question.QuestionViews = _serviceModelFactory.GetOfType<List<QuestionView>>();
                foreach (string id in from.ViewerIds)
                {
                    question.QuestionViews.Add(new QuestionView()
                    {
                        UserId = id,
                        QuestionId = question.Id
                    });
                }
            }
            // Copy Tags
            var tagNamesArray = from.Tags.Split(',');
            for (int i = 0; i<tagNamesArray.Length; i++)
            {
                var questionTag = _domainModelFactory.GetQuestionTag();
                questionTag.TagId = String.Copy(tagNamesArray[i]);
                if (!String.IsNullOrWhiteSpace(question.Id))
                {
                    questionTag.QuestionId = String.Copy(question.Id);
                }
                question.QuestionTags.Add(questionTag);
            }
            // Copy Subscribers
            foreach (IQuestionSubscriber s in from.Subscribers)
            {
                var sub = _serviceModelFactory.GetOfType<Domain.Subscription>();
                sub.UserId = String.Copy(s.Id);
                if (!s.NewSubscriber)
                {
                    sub.QuestionId = from.Id;
                }
                question.Subscriptions.Add(sub);
            }
            return question;
        }

        public Domain.Answer Convert(ServiceModel.Answer from)
        {
            // Get a new instance of Answer
            var answer = _domainModelFactory.GetAnswer();
            // *******************
            // Copy Post part
            // *******************
            AddPostPart(from, answer);
            // *******************
            // Copy VotedPost part
            // *******************
            AddVotedPost(from, answer);
            // *******************
            // Copy Answer part
            // *******************
            // Copy QuestionId
            if (!String.IsNullOrWhiteSpace(from.QuestionId))
            {
                answer.QuestionId = String.Copy(from.QuestionId);
            }
            // Copy accapted
            answer.IsAccepted = from.IsAccepted;
            return answer;
        }

        public Domain.Comment Convert(ServiceModel.Comment from)
        {
            var to = _domainModelFactory.GetComment();
            // *******************
            // Copy Post part
            // *******************
            AddPostPart(from, to);
            if (!String.IsNullOrWhiteSpace(from.VotedPostId))
            {
                to.PostId = String.Copy(from.VotedPostId);
            }
            return to;
        }

        public ApplicationUser Convert(User from)
        {
            var applicationUser = _serviceModelFactory.GetOfType<ApplicationUser>();
            applicationUser.Address = from.Address ?? String.Copy(from.Address);
            applicationUser.Email = from.Email ?? String.Copy(from.Email);
            applicationUser.Id = String.Copy(from.Id);
            applicationUser.ImageUri = from.ImageUri ?? String.Copy(from.ImageUri);
            applicationUser.Points = from.Points;
            applicationUser.UserName = from.Name ?? String.Copy(from.Name);

            return applicationUser;
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
            var domainComments = _serviceModelFactory.GetOfType<List<Domain.Comment>>();
            foreach (ServiceModel.Comment c in from)
            {
                domainComments.Add(Convert(c));
            }
            return domainComments;
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

        public ServiceModel.Subscription Convert(Domain.Subscription from)
        {
            var sub = _serviceModelFactory.GetOfType<ServiceModel.Subscription>();
            sub.UserEmail = String.Copy(from.Subscriber.Email);
            sub.UserId= String.Copy(from.Subscriber.Id);
            sub.UserName = String.Copy(from.Subscriber.UserName);
            return sub;
        }

        public Domain.Subscription Convert(ServiceModel.Subscription from)
        {
            throw new NotImplementedException();
        }

        public List<ServiceModel.Subscription> Convert(ICollection<Domain.Subscription> from)
        {
            var subs = _serviceModelFactory.GetOfType<List<ServiceModel.Subscription>>();
            foreach (Domain.Subscription sub in from)
            {
                subs.Add(Convert(sub));
            }

            return subs;
        }

        public List<Domain.Subscription> Convert(ICollection<ServiceModel.Subscription> from)
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
                to.PublisherName = String.Copy(from.Publisher.UserName);
            }
            if (!String.IsNullOrWhiteSpace(from.PublisherId))
            {
                to.PublisherId = String.Copy(from.PublisherId);
            }            
        }

        private void AddPostPart(ServiceModel.Post from, Domain.Post to)
        {
            // Copy Id
            if (!String.IsNullOrWhiteSpace(from.Id))
            {
                to.Id = String.Copy(from.Id);
            }
            // Copy DatePostd
            to.DatePosted = from.DatePosted;
            // Copy Body
            if (!String.IsNullOrWhiteSpace(from.Body))
            {
                to.Body = String.Copy(from.Body);
            }
            // Copy PublisherId
            if (!String.IsNullOrWhiteSpace(from.PublisherId))
            {
                to.PublisherId = String.Copy(from.PublisherId);
            }
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
                foreach (var flag in from.PostFlags)
                {
                    to.FlaggerIds.Add(flag.Id, flag.UserId);
                }
            }
            // Copy Review
            to.Review = from.Review;
            // Copy ReviewDate
            to.ReviewDate = from.ReviewDate;
        }

        private void AddVotedPost(ServiceModel.VotedPost from, Domain.VotedPost to)
        {
            // Copy DeleteVotes
            if (from.DeleterIds != null)
            {
                to.DeleteVotes = _serviceModelFactory.GetOfType<List<DeleteVote>>();
                foreach (string id in from.DeleterIds)
                {
                    to.DeleteVotes.Add(new DeleteVote()
                    {
                        UserId = id,
                        PostId = to.Id
                    });
                }
            }
            // Copy Flags
            if (from.FlaggerIds != null)
            {
                to.PostFlags = _serviceModelFactory.GetOfType<List<Domain.PostFlag>>();
                foreach (var id in from.FlaggerIds)
                {
                    to.PostFlags.Add(new Domain.PostFlag()
                    {
                        Id = !String.IsNullOrWhiteSpace(id.Key) ? id.Key : null,
                        UserId = id.Value,
                        PostId = to.Id
                    });
                }
            }
            // Copy Votes
            if (from.VoterIds != null)
            {
                to.Votes = _serviceModelFactory.GetOfType<List<Vote>>();
                foreach (string id in from.VoterIds)
                {
                    to.Votes.Add(new Vote()
                    {
                        PostId = to.Id,
                        VoterId = id
                    });
                }
            }
            // Copy Comments 
            if (from.Comments != null)
            {
                to.Comments = Convert(from.Comments);
            }
            // Copy Review
            to.Review = from.Review;
            // Copy ReviewDate
            to.ReviewDate = from.ReviewDate;
        }
    }
}
