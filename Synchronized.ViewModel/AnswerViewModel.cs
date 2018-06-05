using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.ViewModel
{
    public class AnswerViewModel
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public DateTime DatePosted { get; set; }
        public string PublisherName { get; set; }
        public string PublisherId { get; set; }
        public string QuestionId { get; set; }
        public int Points { get; set; }
        public bool Accapted { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }
    }
}
