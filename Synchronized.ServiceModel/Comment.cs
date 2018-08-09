namespace Synchronized.ServiceModel
{
    public class Comment: Post
    {
        /// <summary>
        /// This class represents a Comment in the Business Layer.
        /// </summary>
        public string VotedPostId { get; set; }
    }
}
