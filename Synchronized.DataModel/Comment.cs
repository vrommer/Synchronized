namespace Synchronized.Model
{
    public class Comment : Post
    {
        public string PostId { get; set; }

        public CommentedPost DataPost { get; set; }
    }
}