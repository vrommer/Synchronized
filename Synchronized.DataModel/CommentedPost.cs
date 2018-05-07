using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Synchronized.Model
{
    public abstract class CommentedPost: Post 
    {
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Vote> Votes { get; set; }

        public static explicit operator CommentedPost(Task<CommentedPost> v)
        {
            throw new NotImplementedException();
        }
    }
}
