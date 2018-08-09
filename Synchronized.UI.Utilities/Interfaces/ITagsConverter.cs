using Synchronized.SharedLib.Interfaces;

namespace Synchronized.UI.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from ServiceModel.Tag, TagViewModel and from TagViewModel to ServiceModel.Tag.
    /// </summary>
    public interface ITagsConverter: IDataConverter<ViewModel.TagsViewModels.TagViewModel, ServiceModel.Tag>
    {
    }
}

