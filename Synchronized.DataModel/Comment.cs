namespace Synchronized.Domain
{
    public class Comment : Post
    {
        public string PostId { get; set; }

        public VotedPost VotedPost { get; set; }
    }
}