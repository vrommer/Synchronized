using Synchronized.Domain;
using System.Collections.Generic;

namespace Synchronized.SharedLib.Interfaces
{
    public interface IDataConverter<TSource, TTarget> where TSource: class
        where TTarget : class
    {
        TTarget Convert(TSource from);
        List<TTarget> Convert(List<TSource> from);
    }
}
