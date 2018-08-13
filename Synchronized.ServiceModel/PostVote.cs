using SharedLib.Infrastructure.Constants;

namespace Synchronized.ServiceModel
{
    /// <summary>
    /// This class represents a PostVote on the Business Layer.
    /// </summary>
    public class PostVote
    {
        public VoteType VoteType { get; set; }
        public string UserId { get; set; }
    }
}
