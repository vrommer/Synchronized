using SharedLib.Infrastructure.Constants;
using Synchronized.Core.Utilities.Interfaces;
using System.Linq;
using System.Text;
using Synchronized.Domain;
using Synchronized.ServiceModel;
using Synchronized.Core.Factories.Interfaces;
using System.Collections.Generic;
using System;

namespace Synchronized.Core.Utilities
{
    public class CoreDataConverter : IDataConverter
    {
        private IServiceModelFactory _factory;

        public CoreDataConverter(IServiceModelFactory factory)
        {
            _factory = factory;
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
            var questions = _factory.GetQuestionsList();
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
            var answers = _factory.GetAnswersList();
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
            var comments = _factory.GetCommentsList();
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
            throw new NotImplementedException();
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
            var post = _factory.GetVotedPost();
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

            var to = _factory.GetQuestion();

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
                        builder.Append(",").Append(questionTag.Tag.Name);
                    }
                    else
                    {
                        builder.Append(questionTag.Tag.Name);
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
            var to = _factory.GetAnswer();
            to.IsAccepted = from.IsAccepted;
            // ServiceModel.Post
            AddPostPart(from, to);
            // ServiceModel.VotedPost
            AddVotedPost(from, to);
            return to;
        }

        public ServiceModel.Comment Convert(Domain.Comment from)
        {
            var to = _factory.GetComment();
            to.VotedPostId = String.Copy(from.PostId);
            // ServiceModel.Post 
            AddPostPart(from, to);

            return to;
        }

        public User Convert(ApplicationUser from)
        {
            throw new NotImplementedException();
        }

        public ServiceModel.PostFlag Convert(Domain.PostFlag from)
        {
            throw new NotImplementedException();
        }

        public PostDelete Convert(DeleteVote from)
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
