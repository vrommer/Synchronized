using Synchronized.SharedLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.Core.Utilities.Interfaces
{
    public interface IGenericConverter<T> : IDataConverter<Domain.VotedPost, T> where T: ServiceModel.VotedPost
    {
    }
}
