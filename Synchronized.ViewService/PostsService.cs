using Synchronized.ViewServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Synchronized.ViewModel;
using System.Threading.Tasks;
using Synchronized.ServiceModel;
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

        public Task<string> UpdatePost(EditViewModel post)
        {
            throw new NotImplementedException();
        }
    }
}
