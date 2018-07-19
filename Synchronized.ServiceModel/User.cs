using System.Threading.Tasks;
using Synchronized.ServiceModel.Interfaces;
using System.Collections.Generic;

namespace Synchronized.ServiceModel
{
    public class User: IQuestionSubscriber
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ImageUri { get; set; }
        public int Points { get; set; }
        public bool NewSubscriber { get; set; }
        public List<string> Roles { get; set; }
        public List<Question> Questions { get; set; }

        public Task Update()
        {
            throw new System.NotImplementedException();
        }
    }
}
