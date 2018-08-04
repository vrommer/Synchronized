using System;
using System.Collections.Generic;

namespace Synchronized.Domain
{
    public abstract class VotedPost: Post 
    {
        public bool Review { get; set; }
        public DateTime ReviewDate { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public ICollection<PostFlag> PostFlags { get; set; }
        public ICollection<DeleteVote> DeleteVotes { get; set; }

    }
}
