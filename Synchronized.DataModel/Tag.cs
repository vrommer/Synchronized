using System;
using System.Collections.Generic;

namespace Synchronized.Domain
{
    public class Tag: IEntity
    {
        public string Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublisherId { get; set; }
        public string Description { get; set; }

        public ApplicationUser Publisher { get; set; }
        public ICollection<QuestionTag> QuestionTags{ get; set; }
    }
}