using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Synchronized.Domain
{
    /// <summary>
    /// This class represents the ApplicationUser Entity. Also, this is the IdentityUser for EF Core.
    /// </summary>
    public class ApplicationUser : IdentityUser, IEntity
    {
        public int Points { get; set; }
        public string ImageUri { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Address { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public ICollection<PostFlag> Flags { get; set; }
        public ICollection<QuestionView> QuestionViews { get; set; }
        public ICollection<DeleteVote> DeleteVotes { get; set; }
        public List<Subscription> Subscriptions { get; set; }


    }
}
