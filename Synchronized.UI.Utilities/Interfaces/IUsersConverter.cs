using Synchronized.ServiceModel;
using Synchronized.SharedLib.Interfaces;
using Synchronized.ViewModel.UsersViewModels;

namespace Synchronized.UI.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from ServiceModel.User, UserViewModel and from UserViewModel to ServiceModel.User.
    /// </summary>
    public interface IUsersConverter: IDataConverter<User, UserViewModel>
    {
    }
}
