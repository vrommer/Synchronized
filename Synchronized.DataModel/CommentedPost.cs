using System.Collections.Generic;

namespace Synchronized.Model
{
    public abstract class CommentedPost: Post 
    {
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}
