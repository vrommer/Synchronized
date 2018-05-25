using Synchronized.Domain;

namespace Synchronized.SharedLib.Interfaces
{
    public interface IDataConverter<TSource, TTarget> where TSource: class
        where TTarget : class
    {
        TTarget Convert(TSource from, TTarget to);
    }
}
