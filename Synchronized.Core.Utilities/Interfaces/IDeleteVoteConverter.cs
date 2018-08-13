using Synchronized.SharedLib.Interfaces;

namespace Synchronized.Core.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from Domain.DeleteVote to ServiceModel.PostDelete and from ServiceModel.PostDelete to Domain.DeleteVote.
    /// </summary>
    public interface IDeleteVoteConverter: IDataConverter<Domain.DeleteVote, ServiceModel.PostDelete>
    {
    }
}
