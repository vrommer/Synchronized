using System.Text;

namespace Synchronized.Model
{
    public class Vote
    {
        public string VoterId { get; set; }
        public string PostId { get; set; }
        public int VoteType { get; set; }

        public ApplicationUser Voter { get; set; }
        public CommentedPost Post { get; set; }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Vote vote = obj as Vote;
            if (vote == null) return false;
            else return Equals(vote);
        }

        public override string ToString()
        {
            return (new StringBuilder()
                .Append("User id: ")
                .Append(VoterId)
                .Append("\nQuestion id: ")
                .Append(PostId)
                .ToString());
        }

        public bool Equals(Vote other)
        {
            if (other == null) return false;
            return (GetHashCode() == other.GetHashCode());
        }
    }
}
