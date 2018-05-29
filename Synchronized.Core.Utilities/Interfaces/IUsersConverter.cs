using Synchronized.SharedLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.Core.Utilities.Interfaces
{
    public interface IUserConverter: IDataConverter<Domain.ApplicationUser, ServiceModel.User>
    {

    }
}
