using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel.TagsViewModels;
using Synchronized.ViewServices.Interfaces;
using System;
using Synchronized.UI.Utilities.Interfaces;
using Synchronized.ViewModelFactories.Interfaces;

namespace Synchronized.ViewServices
{
    /// <summary>
    /// This service provides functionality for working with tags.
    /// </summary>
    public class TagsService : ITagsService
    {    
        private int pageSize = 20;
        private Core.Interfaces.ITagsService _tagsService;
        private ITagsConverter _converter;
        private IViewModelFactory _factory;

        public TagsService(Core.Interfaces.ITagsService tagsService, ITagsConverter dataConverter, IViewModelFactory factory) {
            _tagsService = tagsService;
            _converter = dataConverter;
            _factory = factory;                
        }

        public PaginatedList<TagViewModel> GetIndexPage(int pageIndex, string searchTerm)
        {
            string sortOrder = null;
            var tags = _tagsService.GetTagsPage(pageIndex, pageSize, sortOrder, searchTerm);
            var tagsPage = _factory.GetPaginatedList<TagViewModel>(tags.TotalSize, pageIndex, pageSize);

            tags.ForEach(t => {
                var viewTag = _converter.Convert(t);
                tagsPage.Add(viewTag);
            });
            return tagsPage;
        }
    }
}
