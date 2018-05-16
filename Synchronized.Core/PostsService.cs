using Synchronized.Core.Interfaces;
using Synchronized.Model;
using Synchronized.Repository.Interfaces;
using System;
using Synchronized.ServiceModel;
using System.Threading.Tasks;
using SharedLib.Infrastructure.Constants;

namespace Synchronized.Core
{
    public class PostsService : DataService<ServiceModel.Post, Model.Post>, IPostsService
    {

        public PostsService(IDataRepository<Model.Post> repo) : base(repo)
        {
        }

        public async Task VoteForPost(ServiceModel.VotedPost post, ApplicationUser user, VoteType voteType)
        {
            var canVote = CanVote(user, post);
            await VoteForPost(post, user, canVote, voteType);
        }

        public object CommentOnPost(ApplicationUser user, ServiceModel.Comment comment)
        {
            throw new NotImplementedException();
        }

        //private async Task VoteUpPost(ApplicationUser user, ServiceModel.VotedPost post, bool canVote)
        //{
        //    await VoteForPost(post, user, canVote, VoteType.UpVote);
        //}

        private async Task VoteForPost(ServiceModel.VotedPost post, ApplicationUser user, bool canVote, VoteType voteType)
        {
            if (canVote)
            {
                // Update vote in repository
                await VoteForPost(post.Id, (int)voteType, user.Id);
                // Update vote in ServiceMode.Post
                VoteForPost(post, (int)voteType, user.Id);
            }
            else
            {
                await Task.Delay(300);
            }
        }

        private async Task VoteForPost(string postId, int voteType, string votedId) {
            var post = await ((IPostsRepository)_repo).FindPostOfType<Model.VotedPost>(p => p.Id.Equals(postId));
            post.Votes.Add(new Model.Vote
            {
                VoterId = votedId,
                VoteType = voteType
            });
            _repo.Update(post);
        }

        private void VoteForPost(ServiceModel.VotedPost post, int voteType, string votedId)
        {
            post.VoterIds.Add(votedId);
            post.UpVotes = (voteType == (int)VoteType.UpVote) ? ++post.UpVotes : --post.UpVotes;
        }

        private bool CanVote(ApplicationUser user, ServiceModel.VotedPost post)
        {
            if (user == null || post == null)
            {
                return false;
            }
            return !post.VoterIds.Contains(user.Id);
        }
    }
}
