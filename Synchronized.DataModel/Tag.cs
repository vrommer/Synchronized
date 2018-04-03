using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Synchronized.Model
{
    public class Tag
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublisherId { get; set; }
        public string Description { get; set; }

        public virtual ApplicationUser Publisher { get; set; }
    }
}