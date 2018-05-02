using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Synchronized.Model { 

    public class Answer : CommentedPost
    {
        public string QuestionId { get; set; }
        public int Points { get; set; }
        public bool IsAccepted { get; set; }

        public Question Question { get; set; }
    }
}