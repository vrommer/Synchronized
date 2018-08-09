namespace Synchronized.Domain
{
    /// <summary>
    /// This class represents a Comment Database Entity.
    /// </summary>
    public class Comment : Post
    {
        public string PostId { get; set; }

        public VotedPost VotedPost { get; set; }
    }
}