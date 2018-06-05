namespace Synchronized.ServiceModel
{
    public class Answer: VotedPost
    {
        //public int Points { get; set; }
        public string QuestionId { get; set; }
        public bool IsAccepted { get; set; }
    }
}
