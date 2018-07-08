using System;
using Synchronized.Core.Factories.Interfaces;
using Synchronized.Core.Utilities.Interfaces;
using Synchronized.Domain;
using Synchronized.Repository.Interfaces;
using Synchronized.Core.Interfaces;
using System.Threading.Tasks;
using SharedLib.Infrastructure.Constants;
using System.Linq.Expressions;
using Synchronized.ServiceModel;
using Microsoft.Extensions.Logging;
using Synchronized.SharedLib;

namespace Synchronized.Core
{
    public class VotedPostsService : PostsService<Domain.VotedPost, ServiceModel.VotedPost>, IVotedPostService
    {
        public VotedPostsService(IVotedPostRepository repo, IServiceModelFactory factory, IDataConverter converter, ILogger<VotedPostsService> logger) : base(repo, factory, converter, logger)
        {
        }

        public async override Task<bool> Update(ServiceModel.VotedPost post)
        {
            var domainPost = await _repo.GetById(post.Id);
            domainPost.Body = String.Copy(post.Body);
            if (domainPost.GetType().Equals(typeof(Domain.Question)))
            {
                ((Domain.Question)domainPost).Title = String.Copy(((ServiceModel.Question)post).Title);
                //((Domain.Question)domainPost).QuestionTags = _converter.Convert(((ServiceModel.Question)post).Tags);
            }
            await _repo.UpdateAsync(domainPost);
            return true;
        }

        public async override Task<ServiceModel.VotedPost> GetById(string id)
        {
            var domainPost = await _repo.GetById(id);
            // If this domainPost is of type Domain.Question
            if (domainPost.GetType().Equals(typeof(Domain.Question))) {
                var corePost = _converter.Convert((Domain.Question)domainPost);
                return corePost; // var Can only be assigned once and cannot be declared or re-assigned
            }
            // If this domainPost is of type Domain.Answer
            else if (domainPost.GetType().Equals(typeof(Domain.Answer)))
            {
                var corePost = _converter.Convert((Domain.Answer)domainPost);
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
            ServiceModel.Question serviceQuestion;
            //Domain.Question updatedDomainQuestion;
            var post = await((IVotedPostRepository)_repo).GetById(postId);
            var canComment = ((!String.IsNullOrEmpty(userId)) && (!String.IsNullOrEmpty(userId)) && Constants.COMMENT_POINTS <= userPoints);
            var comment = new Domain.Comment();
            if (canComment)
            {
                if (post.GetType().Equals(typeof(Domain.Question)))
                {
                    await SubscribeUser((Domain.Question)post, userId);
                    //serviceQuestion = _converter.Convert((Domain.Question)post);
                }
                else
                {
                    var domainQuestion = await ((IVotedPostRepository)_repo).GetById(((Domain.Answer)post).QuestionId);
                    await SubscribeUser((Domain.Question)domainQuestion, userId);
                    //serviceQuestion = _converter.Convert((Domain.Question)domainQuestion);
                    //var user = _factory.GetUser();
                    //user.Id = userId;
                    //serviceQuestion.Subscribe(user);
                    //var updatedDomainQuestion = _converter.Convert(serviceQuestion);
                    //await _repo.UpdateAsync(updatedDomainQuestion);
                }
                comment.PublisherId = userId;
                comment.Body = commentBody;
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
            var post = await((IVotedPostRepository)_repo).GetById(postId);
            var canDelete = (Constants.DELETE_POINST <= userPoints && !String.IsNullOrEmpty(userId) && 
                !(post.DeleteVotes.Contains(new DeleteVote
            {
                PostId = postId,
                UserId = userId
            })));
            if (canDelete)
            {
                post.DeleteVotes.Add(new DeleteVote
                {
                    UserId = userId
                });
                var serviceQuestion = _converter.Convert((Domain.Question)post);
                await _repo.UpdateAsync(post);                
                await serviceQuestion.Notify();
            }            

            return canDelete;
        }

        public async Task<bool> FlagPost(string postId, string userId, int userPoints)
        {
            var post = await ((IVotedPostRepository)_repo).GetById(postId);
            var canFlag = (Constants.MARK_FOR_REVIEW_POINTS <= userPoints && !String.IsNullOrEmpty(userId) && !(post.PostFlags.Contains(new Domain.PostFlag
            {
                PostId = postId,
                UserId = userId
            })));
            if (canFlag)
            {
                if (post.GetType().Equals(typeof(Domain.Question)))
                {
                    var serviceQuestion = _converter.Convert((Domain.Question)post);
                    await serviceQuestion.Notify();
                }
                else
                {
                    var domainQuestion = await ((IVotedPostRepository)_repo).GetById(((Domain.Answer)post).QuestionId);
                    var serviceQuestion = _converter.Convert((Domain.Question)domainQuestion);
                    await serviceQuestion.Notify();
                }
                post.PostFlags.Add(new Domain.PostFlag
                {
                    UserId = userId
                });
                await _repo.UpdateAsync(post);
            }

            return canFlag;
        }

        public Task<ServiceModel.VotedPost> GetVotedPostBy(Expression<Func<ServiceModel.VotedPost, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<VotedPost> VoteForPost<VotedPost>(string postId, VoteType voteType, string userId)
        {
            throw new NotImplementedException();
        }

        private async Task SubscribeUser(Domain.Question question, String userId)
        {
            Domain.Question updatedQuestion;
            var serviceQuestion = _converter.Convert((question));
            var user = _factory.GetUser();
            user.Id = userId;
            serviceQuestion.Subscribe(user);
            updatedQuestion = _converter.Convert(serviceQuestion);
            await _repo.UpdateAsync(updatedQuestion);
        }
    }

}
