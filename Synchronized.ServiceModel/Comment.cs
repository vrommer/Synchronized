using System.Collections.Generic;

namespace Synchronized.ServiceModel
{
    public class Comment: Post
    {
        public string VotedPostId { get; set; }
        //public ICollection<string> DeleterIds { get; set; }
    }
}
