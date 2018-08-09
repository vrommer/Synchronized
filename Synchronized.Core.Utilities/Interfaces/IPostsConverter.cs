using Synchronized.SharedLib.Interfaces;

namespace Synchronized.Core.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from Domain.Post to ServiceModel.Post and from ServiceModel.Post to Domain.Post.
    /// </summary>
    public interface IPostConverter : IDataConverter<Domain.Post, ServiceModel.Post>
    {        
    }
}
