using Synchronized.Core.Interfaces;
using Synchronized.Model;
using Synchronized.Repository.Interfaces;
using System;
using Synchronized.ServiceModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using SharedLib.Infrastructure.Constants;

namespace Synchronized.Core
{
    public class PostsService : DataService<ServiceModel.Post, Model.Post>, IPostsService
    {

        public PostsService(IDataRepository<Model.Post> repo) : base(repo)
        {
        }

        public object CommentOnPost(ApplicationUser user, ServiceModel.Comment comment)
        {
            throw new NotImplementedException();
        }

        private async Task VoteUpPost(ApplicationUser user, ServiceModel.VotedPost post, bool canVote)
        {
            if (canVote)
            {
                var votedPost = await ((IPostsRepository)_repo).FindPostOfType<Model.VotedPost>(p => p.Id.Equals(post.Id));
                votedPost.Votes.Add(new Model.Vote {
                    VoterId = user.Id,
                    VoteType = (int)VoteType.UpVote
                });
                _repo.Update(votedPost);
                post.UpVotes++;
            }
        }

        private bool CanVote(ApplicationUser user, ServiceModel.VotedPost post)
        {
            if (user == null || post == null)
            {
                return false;
            }
            return !post.VoterIds.Contains(user.Id);
        }

        private User GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public async Task VoteUpPost(ServiceModel.VotedPost post, ApplicationUser user)
        {
            var canVote = CanVote(user, post);
            await VoteUpPost(user, post, canVote);
        }

        public Task VoteDownPost(ServiceModel.VotedPost post, ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
