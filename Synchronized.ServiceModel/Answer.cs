namespace Synchronized.ServiceModel
{
    public class Answer: VotedPost
    {
        public string QuestionId { get; set; }
        public string QuestionPublisherId { get; set; }
        public bool IsAccepted { get; set; }
    }
}
