using System;
using Synchronized.Core.Factories.Interfaces;
using Synchronized.Core.Utilities.Interfaces;
using Synchronized.Domain;
using Synchronized.Repository.Interfaces;
using Synchronized.Core.Interfaces;
using System.Threading.Tasks;
using SharedLib.Infrastructure.Constants;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Synchronized.SharedLib;
using System.Collections.Generic;

namespace Synchronized.Core
{
    public class VotedPostsService : PostsService<Domain.VotedPost, ServiceModel.VotedPost>, IVotedPostService
    {
        private IQuestionsRepository _questionsRepo;

        public VotedPostsService(IVotedPostRepository repo, IQuestionsRepository questionsRepo, IServiceModelFactory factory, IDataConverter converter, ILogger<VotedPostsService> logger) : base(repo, factory, converter, logger)
        {
            _questionsRepo = questionsRepo;
        }

        public async override Task<bool> Update(ServiceModel.VotedPost post)
        {
            _converter.Convert(post);
            var domainPost = await _repo.GetById(post.Id);
            domainPost.Body = String.Copy(post.Body);
            if (domainPost.GetType().Equals(typeof(Question)))
            {
                ((Question)domainPost).Title = String.Copy(((ServiceModel.Question)post).Title);
            }
            domainPost.Review = false;
            domainPost.ReviewDate = DateTime.Now;
            await _repo.UpdateAsync(domainPost);
            return true;
        }

        public async override Task<ServiceModel.VotedPost> GetById(string id)
        {
            var domainPost = await _repo.GetById(id);           
            if (domainPost.GetType().Equals(typeof(Question))) {
                var corePost = _converter.Convert((Question)domainPost);
                return corePost; // var Can only be assigned once and cannot be declared or re-assigned
            }
            // If this domainPost is of type Domain.Answer
            else if (domainPost.GetType().Equals(typeof(Answer)))
            {
                var corePost = _converter.Convert((Answer)domainPost);
                return corePost;
            }
            // In every other case treat domainPost as a Domain.VotedPost
            else
            {
                var corePost = _converter.Convert(domainPost);
                return corePost;
            }

        }

        public async Task<ServiceModel.Comment> CommentOnPost(string postId, string commentBody, string userId, string userName, int userPoints)
        {
            var post = await((IVotedPostRepository)_repo).GetById(postId);
            Question domainQuestion = null;
            Answer domainANswer = null;
            var canComment = ((!String.IsNullOrEmpty(userId)) && (!String.IsNullOrEmpty(userId)) && Constants.COMMENT_POINTS <= userPoints);
            var comment = new Comment();
            if (canComment)
            {
                comment.PublisherId = userId;
                comment.Body = commentBody;
                if (post.GetType().Equals(typeof(Question)))
                {
                    // Get Question with Subscriptions for adding the comment
                    domainQuestion = await _questionsRepo.GetQuestionById(postId);
                    // Add comment to domainQuestion
                    domainQuestion.Comments.Add(comment);
                }
                else
                {
                    // Get Question with Subscriptions
                    domainQuestion = await _questionsRepo.GetQuestionById(((Answer)post).QuestionId);
                    // Get Answer for adding the comment

                }
                SubscribeUser(domainQuestion, userId);
                //await _repo.UpdateAsync(domainQuestion);
                post.Comments.Add(comment);
                await _repo.UpdateAsync(post);
                var serviceModelComment = _converter.Convert(comment);
                serviceModelComment.PublisherName = String.Copy(userName);
                serviceModelComment.DatePosted = comment.DatePosted;
                return serviceModelComment;
            }
            return null;
        }

        public override async Task<bool> DeletePost(string postId, string userId, int userPoints)
        {
            ServiceModel.Question serviceQuestion;
            var post = await((IVotedPostRepository)_repo).GetById(postId);
            var canDelete = (Constants.DELETE_POINST <= userPoints && !String.IsNullOrEmpty(userId) && 
                !(post.DeleteVotes.Contains(new DeleteVote
            {
                PostId = postId,
                UserId = userId
            })));
            if (canDelete)
            {
                if (post.GetType().Equals(typeof(Question)))
                {
                    serviceQuestion = _converter.Convert((Question)post);
                }                
                else
                {
                    var domainQuestion = await _questionsRepo.GetQuestionById(((Domain.Answer)post).QuestionId);
                    serviceQuestion = _converter.Convert(domainQuestion);
                }
                await DeletePost(post, userId);
                await serviceQuestion.Notify();
            }            

            return canDelete;
        }

        private async Task DeletePost(VotedPost post, string userId)
        {
            post.DeleteVotes.Add(new DeleteVote
            {
                UserId = userId
            });
            await _repo.UpdateAsync(post);
        }

        public async Task<bool> FlagPost(string postId, string userId, int userPoints)
        {
            var post = await ((IVotedPostRepository)_repo).GetById(postId);
            var canFlag = (Constants.MARK_FOR_REVIEW_POINTS <= userPoints && !String.IsNullOrEmpty(userId));
            if (canFlag)
            {
                await FlagPost(post, userId);
            }

            return canFlag;
        }

        private async Task FlagPost(VotedPost post, string userId)
        {
            ServiceModel.Question serviceQuestion;
            Question domainQuestion = null;
            ServiceModel.Answer serviceAnswer = null;
            //Answer domainAnswer = null;
            var user = _factory.GetUser();
            user.Id = userId;
            // if post is a question
            if (post.GetType().Equals(typeof(Question)))
            {
                // Get the question with Subscriptions
                domainQuestion = await _questionsRepo.GetQuestionById(post.Id);
                // Convert the question to service model
                serviceQuestion = _converter.Convert(domainQuestion);
                // Add a new Flag to the Question. Will alsways be added.
                serviceQuestion.FlaggerIds.Add("", user.Id);
                // Update the Review indicator
                serviceQuestion.Review = true;
                // Set Review DateTime
                serviceQuestion.ReviewDate = DateTime.Now;
            }
            // If post is Answer
            else
            {
                // Get the parent domain model Question
                domainQuestion = await _questionsRepo.GetQuestionById(((Answer)post).QuestionId);
                // Convert it to service model Question
                serviceQuestion = _converter.Convert(domainQuestion);
                // Find the Answer
                foreach (var answer in serviceQuestion.Answers)
                {
                    if (answer.Id.Equals(post.Id))
                    {
                        serviceAnswer = answer;
                    }
                }
                // Add a new Flag to the Answer. Will alsways be added.
                if (serviceAnswer != null)
                {
                    serviceAnswer.FlaggerIds.Add("", userId);
                    // Update the Review indicator
                    serviceAnswer.Review = true;
                    // Set Review DateTime
                    serviceAnswer.ReviewDate = DateTime.Now;
                }
            }
            // Notify all Question subscribers
            await serviceQuestion.Notify();
            // Call subscribe User
            serviceQuestion.Subscribe(user);
            // Convert the question back to domain model
            domainQuestion = _converter.Convert(serviceQuestion);
            // Send all changes to the database
            await _repo.UpdateAsync(domainQuestion);
            //throw new NotImplementedException();
        }

        public Task<ServiceModel.VotedPost> GetVotedPostBy(Expression<Func<ServiceModel.VotedPost, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<VotedPost> VoteForPost<VotedPost>(string postId, VoteType voteType, string userId)
        {
            throw new NotImplementedException();
        }

        private void SubscribeUser(Question question, String userId)
        {
            Question updatedQuestion;
            var serviceQuestion = _converter.Convert((question));
            var user = _factory.GetUser();
            user.Id = userId;
            serviceQuestion.Subscribe(user);
            updatedQuestion = _converter.Convert(serviceQuestion);
            _repo.UpdateAsync(updatedQuestion);
        }
    }

}
