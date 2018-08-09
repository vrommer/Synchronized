using Synchronized.ServiceModel;
using Synchronized.SharedLib.Interfaces;
using Synchronized.ViewModel.QuestionsViewModels;

namespace Synchronized.UI.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from ServiceModel.Question, QuestionForQuestionsPage and from QuestionForQuestionsPage to ServiceModel.Question.
    /// </summary>
    public interface IQuestionsConverter: IDataConverter<Question, QuestionForQuestionsPage>
    {
    }
}
