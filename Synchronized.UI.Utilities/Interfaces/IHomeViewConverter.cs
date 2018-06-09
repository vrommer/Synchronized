using Synchronized.ServiceModel;
using Synchronized.SharedLib.Interfaces;
using Synchronized.ViewModel.QuestionsViewModels;

namespace Synchronized.UI.Utilities.Interfaces
{
    public interface IHomeViewConverter: IDataConverter<Question, QuestionForHomePage>
    {
    }
}
