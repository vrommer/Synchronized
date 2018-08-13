using Synchronized.ServiceModel;
using Synchronized.SharedLib.Interfaces;
using Synchronized.ViewModel;

namespace Synchronized.UI.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from ServiceModel.VotedPost, EditViewModel and from EditViewModel to ServiceModel.VotedPost.
    /// </summary>
    public interface IEditConverter: IDataConverter<VotedPost, EditViewModel>
    {
    }
}
