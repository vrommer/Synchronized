using System;

namespace Synchronized.ViewModel
{
    /// <summary>
    /// This class contains Comment data for the presentation Layer.
    /// </summary>
    public class CommentViewModel
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public DateTime DatePosted { get; set; }
        public string PublisherName { get; set; }
        public string PublisherId { get; set; }
        public string VotedPostId { get; set; }
        public string VotedPostPublisherId { get; set; }
    }
}
