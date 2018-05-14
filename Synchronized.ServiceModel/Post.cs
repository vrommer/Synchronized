using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.ServiceModel
{
    public abstract class Post
    {
        public string Id { get; set; }
        public DateTime DatePosted { get; set; }
        public string Body { get; set; }
        
        public User Publisher { get; set; }
        public ICollection<PostFlag> Flags { get; set; }
    }
}
