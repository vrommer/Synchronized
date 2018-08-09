using Synchronized.ServiceModel;
using Synchronized.SharedLib.Interfaces;
using Synchronized.ViewModel;

namespace Synchronized.UI.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from ServiceModel.Comment, CommentViewModel and from CommentViewModel to ServiceModel.Comment.
    /// </summary>
    public interface ICommentConverter : IDataConverter<Comment, CommentViewModel>
    {
    }
}
