using System.Collections.Generic;

namespace Synchronized.ServiceModel
{
    public abstract class VotedPost: Post
    {
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public int SumVotes { get => UpVotes - DownVotes; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<string> VoterIds { get; set; }
        public ICollection<string> DeleterIds { get; set; }
    }
}
