using Synchronized.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Synchronized.Model
{
    public class Question : CommentedPost
    {

        public string Title { get; set; }
        public int Points { get; set; }
        public int Deleted { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public ICollection<QuestionTag> QuestionTags { get; set; }
        public ICollection<QuestionView> QuestionViews { get; set; }
        public ICollection<QuestionFlag> QuestionFlags { get; set; }
        public ICollection<DeleteVote> DeleteVotes { get; set; }


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