using System;
using System.Collections.Generic;
using System.Text;
using Synchronized.Core.Factories.Interfaces;
using Synchronized.Core.Utilities.Interfaces;
using Synchronized.Domain;
using Synchronized.Repository.Interfaces;
using Synchronized.Core.Interfaces;
using System.Threading.Tasks;
using SharedLib.Infrastructure.Constants;
using Synchronized.ServiceModel;
using System.Linq.Expressions;

namespace Synchronized.Core
{
    public class VotedPostsService : PostsService<Domain.VotedPost, ServiceModel.VotedPost>, IVotedPostService
    {
        public VotedPostsService(IVotedPostRepository repo, IServiceModelFactory factory, IDataConverter converter) : base(repo, factory, converter)
        {
        }

        public async Task<ServiceModel.Comment> CommentOnPost(string postId, string commentBody, string userId, string userName)
        {
            var post = await((IVotedPostRepository)_repo).GetById(postId);
            var canComment = ((!String.IsNullOrEmpty(userId)) && (!String.IsNullOrEmpty(userId)));
            var comment = new Domain.Comment();
            if (canComment)
            {
                comment.PublisherId = userId;
                comment.Body = commentBody;
                post.Comments.Add(comment);
                await _repo.UpdateAsync(post);
            }

            var serviceModelComment = _converter.Convert(comment);
            serviceModelComment.PublisherName = String.Copy(userName);
            serviceModelComment.DatePosted = comment.DatePosted;
            return serviceModelComment;
        }

        public async Task<bool> DeletePost(string postId, string userId)
        {
            var post = await((IVotedPostRepository)_repo).GetById(postId);
            var canDelete = (!String.IsNullOrEmpty(userId) && !(post.DeleteVotes.Contains(new DeleteVote
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
            }
            await _repo.UpdateAsync(post);

            return canDelete;
        }

        public async Task<bool> FlagPost(string postId, string userId)
        {

            var post = await ((IVotedPostRepository)_repo).GetById(postId);
            var canFlag = (!String.IsNullOrEmpty(userId) && !(post.PostFlags.Contains(new Domain.PostFlag
            {
                PostId = postId,
                UserId = userId
            })));
            if (canFlag)
            {
                post.PostFlags.Add(new Domain.PostFlag
                {
                    UserId = userId
                });
            }
            await _repo.UpdateAsync(post);

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
    }
}
