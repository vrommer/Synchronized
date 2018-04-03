using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Synchronized.Repository.Model
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int Points { get; set; }
        public string ImageUri { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Address { get; set; }
        private ICollection<Label> Labels;

        public ICollection<Label> GetLabels()
        {
            return Labels;
        }

        public void SetLabels(ICollection<Label> Labels)
        {
            this.Labels = Labels;
        }
    }
}
