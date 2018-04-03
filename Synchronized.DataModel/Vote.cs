namespace Synchronized.Model
{
    public class Vote
    {
        public string VoterId { get; set; }
        public string PostId { get; set; }

        public ApplicationUser Voter { get; set; }
        public Post Post { get; set; }
    }
}
