using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.Model
{
    public class CommentedPost: Post 
    {
        public ICollection<Comment> Comments { get; set; }
    }
}
