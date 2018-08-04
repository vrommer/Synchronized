using System.Collections.Generic;

namespace Synchronized.SharedLib.Interfaces
{
    public interface IDataConverter<TFirst, TSecond> where TFirst: class
        where TSecond : class
    {
        TSecond Convert(TFirst from);
        TFirst Convert(TSecond from);
        List<TSecond> Convert(ICollection<TFirst> from);
        List<TFirst> Convert(ICollection<TSecond> from);
    }
}
