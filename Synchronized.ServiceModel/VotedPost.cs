using System;
using System.Collections.Generic;

namespace Synchronized.ServiceModel
{
    public class VotedPost: Post
    {
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public int SumVotes { get => UpVotes - DownVotes; }
        public bool Review { get; set; }
        public DateTime ReviewDate { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public HashSet<string> VoterIds { get; set; }
        public HashSet<string> UpVotersIds { get; set; }
        public HashSet<string> DownVotersIds { get; set; }
        public ICollection<string> DeleterIds { get; set; }
    }
}
