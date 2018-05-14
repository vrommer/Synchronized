using Synchronized.Core.Interfaces;
using Synchronized.ServiceModel;
using System;
using SharedLib.Infrastructure.Constants;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Synchronized.Core.Factories
{
    public class ModelFactory : IModelFactory
    {
        public Question GetQuestion()
        {
            return new Question();
        }

        public Question GetQuestion(Model.Question question)
        {
            var builder = new StringBuilder();
            bool first = true;

            // Question.QuestionTags
            if (question.QuestionTags != null)
            {
                foreach (var questionTag in question.QuestionTags)
                {
                    // Question.Tags
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

            //var newQuestion = new Question();
            var newQuestion = GetQuestion();

            // Post.Id
            newQuestion.Id = question.Id;
            // Post.DatePosted
            newQuestion.DatePosted = question.DatePosted;
            // Post.Publisher
            if (question.Publisher != null)
            {
                newQuestion.Publisher = GetPublisher(question.Publisher);
            }
            // Question.Title
            newQuestion.Title = question.Title;
            // Post.Body
            newQuestion.Body = question.Body;
            // Question.Tags
            newQuestion.Tags = builder.ToString();
            // Question.Views
            if (question.QuestionViews != null)
            {
                newQuestion.Views = question.QuestionViews.Count();
            }
            // Question.IsAnswered            
            newQuestion.IsAnswered = question.Answered();                                    

            // Post.PostFlags
            newQuestion.Flags = GetPostFlagsList();
            if (question.PostFlags != null)
            {
                foreach (var pf in question.PostFlags)
                {
                    newQuestion.Flags.Add(GetPostFlag(pf));
                }
            }

            // VotedPost.Votes
            newQuestion.VoterIds = GetVoterIdsList();
            if (question.Votes != null)
            {
                foreach (var vote in question.Votes)
                {
                    newQuestion.VoterIds.Add(vote.VoterId);
                    if (vote.VoteType == (int)VoteType.UpVote)
                        newQuestion.UpVotes++;
                    else
                        newQuestion.DownVotes++;
                }
            }

            // VotedPost.Comments
            newQuestion.Comments = GetCommentsList();
            if (question.Comments != null)
            {
                foreach (var comment in question.Comments)
                {
                    newQuestion.Comments.Add(GetComment(comment));
                }
            }

            // Question.Answers
            newQuestion.Answers = GetAnswersList();
            if (question.Answers != null)
            {
                foreach (var a in question.Answers)
                {
                    newQuestion.Answers.Add(GetAnswer(a));
                }
            }

            return newQuestion;
        }

        private ICollection<string> GetVoterIdsList()
        {
            return new List<string>();
        }

        private ICollection<Comment> GetCommentsList()
        {
            return new List<Comment>();
        }

        public User GetPublisher()
        {
            return new User();
        }

        public User GetPublisher(Model.ApplicationUser publisher)
        {
            var newUser= GetPublisher();
            newUser.Name = publisher.UserName;
            newUser.Id = publisher.Id;
            return newUser;
        }

        public Answer GetAnswer ()
        {
            return new Answer();
        }

        public Answer GetAnswer(Model.Answer answer)
        {
            var newAnswer = GetAnswer();
            // Post.Id
            newAnswer.Id = answer.Id;
            // Post.DatePosted
            newAnswer.DatePosted = answer.DatePosted;
            // Post.Body
            newAnswer.Body = answer.Body;
            // Post.Bulisher
            if (answer.Publisher != null)
            {
                newAnswer.Publisher = GetPublisher(answer.Publisher);
            }
            // Answer.Points
            newAnswer.Points = answer.Points;
            // Answer.IsAccepted
            newAnswer.IsAccepted = answer.IsAccepted;

            newAnswer.Flags = GetPostFlagsList();
            newAnswer.Comments = GetCommentsList();

            // Post.PostFlags
            if (answer.PostFlags != null)
            {
                foreach (var pf in answer.PostFlags)
                {
                    newAnswer.Flags.Add(GetPostFlag(pf));
                }
            }

            // VotedPost.Votes
            if (answer.Votes != null)
            {
                foreach (var vote in answer.Votes)
                {
                    if (vote.VoteType == (int)VoteType.UpVote)
                        newAnswer.UpVotes++;
                    else
                        newAnswer.DownVotes++;
                }
            }
            // VotedPost.Comments
            if (answer.Comments != null)
            {
                foreach (var comment in answer.Comments)
                {
                    newAnswer.Comments.Add(GetComment(comment));
                }
            }

            return newAnswer;
        }

        public Comment GetComment()
        {
            return new Comment();
        }

        public Comment GetComment(Model.Comment comment)
        {
            var newComment = GetComment();

            // Post.Id
            newComment.Id = comment.Id;
            // Post.DatePosted
            newComment.DatePosted = comment.DatePosted;
            // Post.Body
            newComment.Body = comment.Body;
            // Comment.VotedPostId
            newComment.VotedPostId = comment.PostId;

            newComment.Flags = GetPostFlagsList();

            // Post.PostFlags
            if (comment.PostFlags != null)
            {
                foreach (var pf in comment.PostFlags)
                {
                    newComment.Flags.Add(GetPostFlag(pf));
                }
            }

            return newComment;
        }

        public PostFlag GetPostFlag ()
        {
            return new PostFlag();
        }

        public PostFlag GetPostFlag(Domain.PostFlag flag)
        {
            var newPostFlag = GetPostFlag();

            newPostFlag.UserId = flag.UserId;

            return newPostFlag;
        }

        public ICollection<PostFlag> GetPostFlagsList()
        {
            return new List<PostFlag>();
        }

        public ICollection<Answer> GetAnswersList()
        {
            return new List<Answer>();
        }
    }
}
