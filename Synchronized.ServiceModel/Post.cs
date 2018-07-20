using Synchronized.Domain;
using System;
using System.Collections.Generic;

namespace Synchronized.ServiceModel
{
    public class Post
    {
        public string Id { get; set; }
        public DateTime DatePosted { get; set; }
        public string Body { get; set; }
        
        public string PublisherName { get; set; }
        public string PublisherId { get; set; }
        public Dictionary<string, string> FlaggerIds { get; set; }
    }
}
