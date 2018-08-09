using Synchronized.SharedLib.Interfaces;

namespace Synchronized.Core.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from Domain.Question to ServiceModel.Question and from ServiceModel.Question to Domain.Question.
    /// </summary>
    public interface IQuestionConverter : IDataConverter<Domain.Question, ServiceModel.Question>
    {        
    }
}
