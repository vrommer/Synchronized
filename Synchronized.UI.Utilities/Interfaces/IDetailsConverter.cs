using Synchronized.ServiceModel;
using Synchronized.SharedLib.Interfaces;
using Synchronized.ViewModel.QuestionsViewModels;

namespace Synchronized.UI.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from ServiceModel.Question, QuestionForDetailsPage and from QuestionForDetailsPage to ServiceModel.Question.
    /// </summary>
    public interface IDetailsConverter: IDataConverter<Question, QuestionForDetailsPage>
    {
    }
}
