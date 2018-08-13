using Synchronized.SharedLib.Interfaces;

namespace Synchronized.Core.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from Domain.Answer to ServiceModel.Answer and from ServiceModel.Answer to Domain.Answer.
    /// </summary>
    public interface IAnswerConverter: IDataConverter<Domain.Answer, ServiceModel.Answer>
    {
    }
}
