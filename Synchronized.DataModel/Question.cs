using System.Collections.Generic;
using System.Linq;

namespace Synchronized.Model
{
    public class Question : CommentedPost
    {

        public string Title { get; set; }
        public int Points { get; set; }
        public int Views { get; set; }

        public ICollection<Answer> Answers { get; set; }
        //public ICollection<Comment> Comments { get; set; }
        public ICollection<QuestionTag> QuestionTags { get; set; }

        public bool Answered() {
            if (Answers == null)
            {
                return false;
            }

            bool answered = false;
            Answers.ToList().ForEach(a => {
                if (a.IsAccepted)
                    answered = true;                    
            });
            return answered;
        }
    }
}