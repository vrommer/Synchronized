using Synchronized.ServiceModel;
using Synchronized.SharedLib.Interfaces;
using Synchronized.ViewModel.QuestionsViewModels;

namespace Synchronized.UI.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from ServiceModel.Question, QuestionForHomePage and from QuestionForHomePage to ServiceModel.Question.
    /// </summary>
    public interface IHomeViewConverter: IDataConverter<Question, QuestionForHomePage>
    {
    }
}
