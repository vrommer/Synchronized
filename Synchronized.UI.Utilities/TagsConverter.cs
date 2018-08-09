using Synchronized.UI.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using Synchronized.ServiceModel;
using Synchronized.ViewModel.TagsViewModels;
using Synchronized.ViewModelFactories.Interfaces;
using Synchronized.Core.Factories.Interfaces;

namespace Synchronized.UI.Utilities
{
    /// <summary>
    /// This is a concrete Converter for converting between ServiceModel Tags and ViewModel types.
    /// </summary>
    public class TagsConverter : ITagsConverter
    {
        private IViewModelFactory _viewModelFactory;
        private IServiceModelFactory _serviceModelFactory;

        public TagsConverter(IViewModelFactory viewModelFactory, IServiceModelFactory serviceModelFactory)
        {
            _viewModelFactory = viewModelFactory;
            _serviceModelFactory = serviceModelFactory;
        }

        public Tag Convert(TagViewModel from)
        {
            throw new NotImplementedException();
        }

        public TagViewModel Convert(Tag from)
        {
            var viewTag = _viewModelFactory.GetTag();
            viewTag.Description = String.Copy(from.Description);
            viewTag.Name = String.Copy(from.Name);
            return viewTag;
        }

        public List<Tag> Convert(ICollection<TagViewModel> from)
        {
            throw new NotImplementedException();
        }

        public List<TagViewModel> Convert(ICollection<Tag> from)
        {
            var tags = _viewModelFactory.GetTags();
            foreach (var t in from)
            {
                tags.Add(Convert(t));
            }

            return tags;
        }
    }
}
