using System.Text;

namespace Synchronized.Domain
{
    public class Subscription
    {
        public Question Question { get; set; }
        public ApplicationUser Subscriber { get; set; }

        public string UserId { get; set; }
        public string QuestionId { get; set; }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Subscription subscription = obj as Subscription;
            if (subscription == null) return false;
            else return Equals(subscription);
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

        public bool Equals(Subscription other)
        {
            if (other == null) return false;
            return (GetHashCode() == other.GetHashCode());
        }
    }
}
