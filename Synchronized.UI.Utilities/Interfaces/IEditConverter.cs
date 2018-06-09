using Synchronized.ServiceModel;
using Synchronized.SharedLib.Interfaces;
using Synchronized.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.UI.Utilities.Interfaces
{
    public interface IEditConverter: IDataConverter<VotedPost, EditViewModel>
    {
    }
}
