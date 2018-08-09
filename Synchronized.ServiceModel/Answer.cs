namespace Synchronized.ServiceModel
{
    /// <summary>
    /// This class represents an Answer in the Business Layer.
    /// </summary>
    public class Answer: VotedPost
    {
        public string QuestionId { get; set; }
        public string QuestionPublisherId { get; set; }
        public bool IsAccepted { get; set; }
    }
}
