using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synchronized.Domain
{
    /// <summary>
    /// This class represents a Question in the Database.
    /// </summary>
    public class Question : VotedPost
    {

        public string Title { get; set; }
        //public int Points { get; set; }
        public int Deleted { get; set; }
        public List<Answer> Answers { get; set; }
        public List<QuestionTag> QuestionTags { get; set; }
        public List<QuestionView> QuestionViews { get; set; }
        public List<Subscription> Subscriptions { get; set; }


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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ID: ")
                .Append(Id)
                .Append("\nTitle: ")
                .Append(Title)
                .Append("\nBody: ")
                .Append(Body.Substring(Math.Min(Body.Length, 56)));
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}