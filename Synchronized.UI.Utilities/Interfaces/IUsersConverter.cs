using Synchronized.ServiceModel;
using Synchronized.SharedLib.Interfaces;
using Synchronized.ViewModel.UsersViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.UI.Utilities.Interfaces
{
    public interface IUsersConverter: IDataConverter<User, UserViewModel>
    {
    }
}
