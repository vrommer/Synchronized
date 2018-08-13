using Synchronized.SharedLib.Interfaces;

namespace Synchronized.Core.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from Domain.ApplicationUser to ServiceModel.User and from ServiceModel.User to Domain.ApplicationUser.
    /// </summary>
    public interface IUserConverter: IDataConverter<Domain.ApplicationUser, ServiceModel.User>
    {

    }
}
