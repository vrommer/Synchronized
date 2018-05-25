using Synchronized.Core.Utilities.Interfaces;
using Synchronized.Domain;
using Synchronized.ServiceModel;

namespace Synchronized.Core.Utilities
{
    public class CoreDataConverter : IDataConverter
    {
        public ServiceModel.Post Convert(Domain.Post from, ServiceModel.Post to)
        {
            throw new System.NotImplementedException();
        }

        public ServiceModel.VotedPost Convert(Domain.VotedPost from, ServiceModel.VotedPost to)
        {
            throw new System.NotImplementedException();
        }

        public ServiceModel.Question Convert(Domain.Question from, ServiceModel.Question to)
        {
            throw new System.NotImplementedException();
        }

        public ServiceModel.Answer Convert(Domain.Answer from, ServiceModel.Answer to)
        {
            throw new System.NotImplementedException();
        }

        public ServiceModel.Comment Convert(Domain.Comment from, ServiceModel.Comment to)
        {
            throw new System.NotImplementedException();
        }
    }
}
