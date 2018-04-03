using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.Repository.Model
{
    public abstract class Post
    {
        public string Id { get; set; }
        public DateTime DatePosted { get; set; }
        public string PublisherId { get; set; }
        public string Content { get; set; }
    }
}
