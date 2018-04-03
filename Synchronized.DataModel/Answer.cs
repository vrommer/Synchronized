using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Synchronized.Model { 

    public class Answer : Post
    {
        public string QuestionId { get; set; }
        public int Points { get; set; }
        public bool IsAccepted { get; set; }

        public Question Question { get; set; }
        public ICollection<Comment> Comments { get; set; }

        //private ICollection<Comment> Comments;

        //public ICollection<Comment> GetComments()
        //{
        //    return Comments;
        //}

        //public void SetComments(ICollection<Comment> Comments)
        //{
        //    this.Comments = Comments;
        //}
    }
}