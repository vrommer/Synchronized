using System.Collections.Generic;

namespace Synchronized.ServiceModel
{
    public class Question: VotedPost
    {
        public string Title { get; set; }
        public string Tags { get; set; }
        public int Views { get; set; }
        public ICollection<string> ViewerIds{ get; set; }
        public bool IsAnswered { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
