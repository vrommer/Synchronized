namespace Synchronized.Domain
{
    /// <summary>
    /// This class represents a QuestionTag Entity in the Database.
    /// </summary>
    public class QuestionTag
    {
        public string QuestionId { get; set; }
        public string TagId { get; set; }

        public Question Question { get; set; }
        public Tag Tag { get; set; }
    }
}