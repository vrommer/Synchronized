using Synchronized.SharedLib.Interfaces;
using System;

namespace Synchronized.Core.Utilities.Interfaces
{
    public interface ICommentConverter : IDataConverter<Domain.Comment, ServiceModel.Comment>
    {
    }
}
