using Synchronized.Domain;
using System;
using System.Text;

namespace Synchronized.Domain
{
    public class PostFlag
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }

        public VotedPost Post { get; set; }
        public ApplicationUser User { get; set; }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            PostFlag questionFlag = obj as PostFlag;
            if (questionFlag == null) return false;
            else return Equals(questionFlag);
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

        public bool Equals(PostFlag other)
        {
            if (other == null) return false;
            return (GetHashCode() == other.GetHashCode());
        }
    }
}
