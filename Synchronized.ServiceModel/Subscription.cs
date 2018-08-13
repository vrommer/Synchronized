using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.ServiceModel
{
    /// <summary>
    /// This class represents a Subscription in the Business Layer.
    /// </summary>
    public class Subscription
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string QuestionId { get; set; }
    }
}
