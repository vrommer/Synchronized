using Microsoft.AspNetCore.Identity;
using Synchronized.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Synchronized.Model
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int Points { get; set; }
        public string ImageUri { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Address { get; set; }

        public ICollection<Vote> Votes { get; set; }
        public ICollection<PostFlag> Flags { get; set; }
        public ICollection<QuestionView> QuestionViews { get; set; }
        public ICollection<DeleteVote> DeleteVotes { get; set; }
    }
}
