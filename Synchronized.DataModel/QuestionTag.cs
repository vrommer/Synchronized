using System;

namespace Synchronized.Model
{
    public class QuestionTag
    {
        public string QuestionId { get; set; }
        public string TagId { get; set; }

        public Question Question { get; set; }
        public Tag Tag { get; set; }
    }
}