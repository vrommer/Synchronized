using SharedLib.Infrastructure.Constants;

namespace Synchronized.ServiceModel
{
    public class PostVote
    {
        public VoteType VoteType { get; set; }
        public string UserId { get; set; }
    }
}
