using Synchronized.ServiceModel;
using Synchronized.SharedLib.Interfaces;
using Synchronized.ViewModel;

namespace Synchronized.UI.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from ServiceModel.Answer, AnswerViewModel and from AnswerViewModel to ServiceModel.Answer.
    /// </summary>
    public interface IAnswerConverter : IDataConverter<Answer, AnswerViewModel>
    {

    }
}
