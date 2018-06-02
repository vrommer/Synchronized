﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Synchronized.Domain
{
    public abstract class VotedPost: Post 
    {
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public ICollection<PostFlag> PostFlags { get; set; }
        public ICollection<DeleteVote> DeleteVotes { get; set; }
    }
}
