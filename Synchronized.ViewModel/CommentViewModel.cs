using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.ViewModel
{
    public class CommentViewModel
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public DateTime DatePosted { get; set; }
        public string PublisherName { get; set; }
        public string PublisherId { get; set; }
        public string VotedPostId { get; set; }
    }
}
