namespace Synchronized.Model
{
    public class Comment : Post
    {
        public string PostId { get; set; }

        public Post Post { get; set; }
    }
}