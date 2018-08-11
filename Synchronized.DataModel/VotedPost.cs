using SharedLib.Infrastructure.Constants;
using Synchronized.SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Synchronized.Domain
{
    /// <summary>
    /// This class represents a VotedPost in the Database.
    /// </summary>
    public abstract class VotedPost: Post 
    {
        public bool Review { get; set; }
        public DateTime ReviewDate { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public ICollection<PostFlag> PostFlags { get; set; }
        public ICollection<DeleteVote> DeleteVotes { get; set; }
        public int SumVotes { get; set; }
    }
}
