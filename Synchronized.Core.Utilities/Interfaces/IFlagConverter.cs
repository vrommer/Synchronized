using Synchronized.SharedLib.Interfaces;

namespace Synchronized.Core.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from Domain.PostFlag to ServiceModel.PostFlag and from ServiceModel.PostFlag to Domain.PostFlag.
    /// </summary>
    public interface IFlagConverter: IDataConverter<Domain.PostFlag, ServiceModel.PostFlag>
    {
    }
}
