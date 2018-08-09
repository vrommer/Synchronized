using Synchronized.ServiceModel;
using Synchronized.SharedLib.Interfaces;
using Synchronized.ViewModel.QuestionsViewModels;

namespace Synchronized.UI.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from ServiceModel.Question, AskViewModel and from AskViewModel to ServiceModel.Question.
    /// </summary>
    public interface IAskConverter: IDataConverter<Question, AskViewModel>
    {
    }
}
