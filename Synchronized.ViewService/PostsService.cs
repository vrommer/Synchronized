using Synchronized.ViewServices.Interfaces;
using System;
using Synchronized.ViewModel;
using System.Threading.Tasks;
using Synchronized.UI.Utilities.Interfaces;
using Synchronized.ViewModelFactories.Interfaces;
using Synchronized.ServiceModel;
using Synchronized.Core.Factories.Interfaces;

namespace Synchronized.ViewServices
{
    /// <summary>
    /// A concrete PostsService.
    /// </summary>
    public class PostsService : IPostsService
    {
        private Core.Interfaces.IVotedPostService _postsService;
        private IPostsConverter _converter;
        private IViewModelFactory _viewFactory;
        private IServiceModelFactory _serviceFactory;

        public PostsService(Core.Interfaces.IVotedPostService postsService, IPostsConverter converter, IViewModelFactory viewFactory, IServiceModelFactory serviceFactory)
        {
            _postsService = postsService;
            _converter = converter;
            _viewFactory = viewFactory;
            _serviceFactory = serviceFactory;
        }

        public async Task<EditViewModel> GetPostForEdit(string postId)
        {
            var servicePost = await _postsService.GetById(postId);
            var viewModel = _converter.Convert(servicePost);
            return viewModel;
        }

        async Task<string> IPostsService.EditPost(EditViewModel post)
        {
            VotedPost corePost;
            if (!String.IsNullOrEmpty(post.Title))
            {
                corePost = _serviceFactory.GetQuestion();
                ((Question)corePost).Title = String.Copy(post.Title);
            }
            else
            {
                corePost = _serviceFactory.GetQuestion();
            }
            corePost.Id = String.Copy(post.Id);
            corePost.Body = String.Copy(post.Body);
            corePost.Review = false;
            await _postsService.Update(corePost);
            if (!String.IsNullOrEmpty(post.Title))
            {      
                return post.Id;
            }
            else
            {
                return post.QuestionId;
            }
        }
    }
}
