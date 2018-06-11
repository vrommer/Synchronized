using Synchronized.ViewServices.Interfaces;
using System;
using Synchronized.ViewModel;
using System.Threading.Tasks;
using Synchronized.UI.Utilities.Interfaces;
using Synchronized.ViewModelFactories.Interfaces;

namespace Synchronized.ViewServices
{
    public class PostsService : IPostsService
    {
        private Core.Interfaces.IVotedPostService _postsService;
        private IPostsConverter _converter;
        private IViewModelFactory _factory;

        public PostsService(Core.Interfaces.IVotedPostService postsService, IPostsConverter converter, IViewModelFactory factory)
        {
            _postsService = postsService;
            _converter = converter;
            _factory = factory;
        }
        public async Task<EditViewModel> GetPostForEdit(string postId)
        {
            var servicePost = await _postsService.GetById(postId);
            var viewModel = _converter.Convert(servicePost);
            return viewModel;
        }

        //public async Task<bool> UpdatePost(EditViewModel post)
        //{
        //    if (!String.IsNullOrEmpty(post.Title))
        //    {
        //        var corePost = _factory.GetOfType<ServiceModel.Question>();
        //        corePost.Id = String.Copy(post.Id);
        //        corePost.Body = String.Copy(post.Body);
        //        corePost.Title = String.Copy(post.Title);
        //        await _postsService.Update(corePost);
        //    }
        //    else
        //    {
        //        var corePost = _factory.GetOfType<ServiceModel.VotedPost>();
        //        corePost.Id = String.Copy(post.Id);
        //        corePost.Body = String.Copy(post.Body);
        //        await _postsService.Update(corePost);
        //    }
        //    return true;
        //}

        async Task<string> IPostsService.UpdatePost(EditViewModel post)
        {

            if (!String.IsNullOrEmpty(post.Title))
            {
                var corePost = _factory.GetOfType<ServiceModel.Question>();
                corePost.Id = String.Copy(post.Id);
                corePost.Body = String.Copy(post.Body);
                corePost.Title = String.Copy(post.Title);
                await _postsService.Update(corePost);
                return post.Id;
            }
            else
            {
                var corePost = _factory.GetOfType<ServiceModel.VotedPost>();
                corePost.Id = String.Copy(post.Id);
                corePost.Body = String.Copy(post.Body);
                await _postsService.Update(corePost);
                return post.QuestionId;
            }
        }
    }
}
