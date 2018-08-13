using System;

namespace Synchronized.Domain
{
    /// <summary>
    /// This class represents a Post in the Database.
    /// </summary>
    public abstract class Post : IEntity
    {
        public string Id { get; set; }
        public DateTime DatePosted { get; set; }
        public string PublisherId { get; set; }
        public string Body { get; set; }
        public ApplicationUser Publisher { get; set; }

    }
}
