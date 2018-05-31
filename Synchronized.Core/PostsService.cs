using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Synchronized.Core.Interfaces;
using Synchronized.Repository.Interfaces;
using Synchronized.Core.Utilities.Interfaces;
using Synchronized.Core.Factories.Interfaces;
using SharedLib.Infrastructure.Constants;
using Synchronized.ServiceModel;

namespace Synchronized.Core
{
    public class PostsService<TEntity, TServiceModel> : DataService<TEntity, TServiceModel>, IPostsService<TServiceModel> 
        where TServiceModel : Post
        where TEntity : Domain.Post
    {

        public PostsService(IDataRepository<TEntity> repo, IServiceModelFactory factory, IDataConverter converter)  : base(repo, factory, converter) 
        {
        }

        public Task<T> CommentOnPost<T>(string postId, string userId, string commentBody) where T: VotedPost
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePost(string postId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<TServiceModel> FlagPost(string postId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<VotedPost> GetVotedPostBy(Expression<Func<VotedPost, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<T> VoteForPost<T>(string postId, VoteType voteType, string userId) where T: VotedPost
        {
            var domainPost = await ((IPostsRepository<Domain.VotedPost>)_repo).GetVotedPostBy(p => p.Id.Equals(postId));
            var serviceModelPost = _converter.Convert(domainPost);
                     
            return (T)serviceModelPost;
        }

        private async Task VoteForPostAsync<T>(bool canVote, VoteType voteType, string userId, T serviceModelPost, Domain.VotedPost domainPost) where T: VotedPost
        {
            if (canVote)
            {
                // Update vote in repository
                await VoteForPost(domainPost, userId, (int)voteType);
                // Update vote in ServiceMode.Post
                VoteForPost(serviceModelPost, userId, (int)voteType);
            }
            else
            {
                await Task.Delay(300);
            }
        }

        public async Task VoteForPost(Domain.VotedPost domainPost, string userId, int voteType) {
            domainPost.Votes.Add(new Domain.Vote
            {
                VoterId = userId,
                VoteType = voteType
            });
            await ((IPostsRepository<Domain.VotedPost>)_repo).UpdateAsync(domainPost);
        }

        private void VoteForPost(VotedPost post, string userId, int voteType) { }

        private bool CanVote<T>(T serviceModelPost, string userId) where T: VotedPost
        {
            if (String.IsNullOrEmpty(userId) || serviceModelPost == null)
            {
                return false;
            }
            return !serviceModelPost.VoterIds.Contains(userId);
        }
    }
}
