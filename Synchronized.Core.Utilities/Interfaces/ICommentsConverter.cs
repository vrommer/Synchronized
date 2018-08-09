using Synchronized.SharedLib.Interfaces;
using System;

namespace Synchronized.Core.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from Domain.Comment to ServiceModel.Comment and from ServiceModel.Comment to Domain.Comment.
    /// </summary>
    public interface ICommentConverter : IDataConverter<Domain.Comment, ServiceModel.Comment>
    {
    }
}
