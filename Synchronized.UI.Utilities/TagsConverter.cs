using Synchronized.UI.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Synchronized.ServiceModel;
using Synchronized.ViewModel.TagsViewModels;
using Synchronized.ViewModelFactories.Interfaces;
using Synchronized.Core.Factories.Interfaces;

namespace Synchronized.UI.Utilities
{
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
            viewTag.Description = from.Description;
            viewTag.Name = from.Name;
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
