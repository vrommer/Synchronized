using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Synchronized.Repository.Model
{
    [Table("Question")]
    public class Question : Post
    {

        public string Title { get; set; }

        public int Points { get; set; }
        public int Views { get; set; }
        public bool AnswerAccepted { get; set; }
        public string LabelNames { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        private ICollection<Comment> Comments;
        private ICollection<Label> Labels;

        public ICollection<Comment> GetComments()
        {
            return Comments;
        }

        public void SetComments(ICollection<Comment> Comments)
        {
            this.Comments = Comments;
        }

        public ICollection<Label> GetLabels()
        {
            return Labels;
        }

        public void SetLabels(ICollection<Label> Labels)
        {
            this.Labels = Labels;
        }

        public bool Answered() {
            bool answered = false;
            Answers.ToList().ForEach(a => {
                if (a.IsAccepted)
                    answered = true;                    
            });
            return answered;
        }
    }
}