namespace Synchronized.Domain
{

    public class Answer : VotedPost
    {
        public string QuestionId { get; set; }
        //public int Points { get; set; }
        public bool IsAccepted { get; set; }

        public Question Question { get; set; }
    }
}