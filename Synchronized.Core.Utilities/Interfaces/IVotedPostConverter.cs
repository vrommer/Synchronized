using Synchronized.SharedLib.Interfaces;

namespace Synchronized.Core.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from Domain.VotedPost to ServiceModel.VotedPost and from ServiceModel.VotedPost to Domain.VotedPost.
    /// </summary>
    public interface IVotedPostConverter : IDataConverter<Domain.VotedPost, ServiceModel.VotedPost>
    {

    }
}
