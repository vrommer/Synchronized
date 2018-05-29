using Synchronized.ServiceModel;
using Synchronized.SharedLib.Interfaces;
using Synchronized.ViewModel.QuestionsViewModels;

namespace Synchronized.UI.Utilities.Interfaces
{
    public interface IDetailsConverter: IDataConverter<Question, QuestionForDetailsPage>
    {
    }
}
