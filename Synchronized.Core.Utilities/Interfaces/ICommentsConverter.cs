using Synchronized.SharedLib.Interfaces;
using System;

namespace Synchronized.Core.Utilities.Interfaces
{
    public interface ICommentsConverter : IDataConverter<Domain.Comment, ServiceModel.Comment>
    {
    }
}
