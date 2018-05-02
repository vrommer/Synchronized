using Synchronized.Model;
using System.Text;

namespace Synchronized.Domain
{
    public class QuestionFlag
    {
        public string UserId { get; set; }
        public string QuestionId { get; set; }

        public Question Question { get; set; }
        public ApplicationUser User { get; set; }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            QuestionFlag questionFlag = obj as QuestionFlag;
            if (questionFlag == null) return false;
            else return Equals(questionFlag);
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

        public bool Equals(QuestionFlag other)
        {
            if (other == null) return false;
            return (GetHashCode() == other.GetHashCode());
        }
    }
}
