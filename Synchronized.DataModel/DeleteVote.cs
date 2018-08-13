using System.Text;

namespace Synchronized.Domain
{
    /// <summary>
    /// This class represents The DeleteVote Database Entity.
    /// </summary>
    public class DeleteVote
    {
        public string PostId { get; set; }
        public string UserId { get; set; }

        public VotedPost Post{ get; set; }
        public ApplicationUser User { get; set; }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            DeleteVote questionView = obj as DeleteVote;
            if (questionView == null) return false;
            else return Equals(questionView);
        }

        public override string ToString()
        {
            return (new StringBuilder()
                .Append("User id: ")
                .Append(UserId)
                .Append("\nQuestion id: ")
                .Append(PostId)
                .ToString());
        }

        public bool Equals(DeleteVote other)
        {
            if (other == null) return false;
            return (GetHashCode() == other.GetHashCode());
        }
    }

}

