using Synchronized.Domain;
using System;
using System.Text;

namespace Synchronized.Domain
{
    public class QuestionView
    {
        public string UserId { get; set; }
        public string QuestionId { get; set; }

        public ApplicationUser User { get; set; }
        public Question Question { get; set; }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            QuestionView questionView = obj as QuestionView;
            if (questionView == null) return false;
            else return Equals(questionView);
        }

        public override string ToString()
        {
            return (new StringBuilder()
                .Append("User id: ")
                .Append(UserId)
                .Append("\nQuestion id: ")
                .Append(QuestionId)
                .ToString());
        }

        public bool Equals(QuestionView other)
        {
            if (other == null) return false;
            return (GetHashCode() == other.GetHashCode());
        }
    }
}
