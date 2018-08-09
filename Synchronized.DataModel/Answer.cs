namespace Synchronized.Domain
{
    /// <summary>
    /// This class represents the Answer Db Entity
    /// </summary>
    public class Answer : VotedPost
    {
        public string QuestionId { get; set; }
        public bool IsAccepted { get; set; }
        public Question Question { get; set; }


    }
}