//using Synchronized.Core.Utilities.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Synchronized.Domain;
//using Synchronized.Core.Factories.Interfaces;
//using Synchronized.Domain.Factories.Interfaces;

//namespace Synchronized.Core.Utilities
//{
//    public class GenericConverter<T> : IGenericConverter<T> where T : ServiceModel.VotedPost
//    {
//        private IServiceModelFactory _serviceModelFactory;
//        private IDomainModelFactory _domainModelFactory;
//        private IDataConverter _converter;

//        public GenericConverter(IServiceModelFactory serviceModelFactory, IDomainModelFactory domainModel, IDataConverter converter)
//        {
//            _serviceModelFactory = serviceModelFactory;
//            _domainModelFactory = domainModel;
//            _converter = converter;
//        }

//        public T Convert(VotedPost from)
//        {
//            throw new NotImplementedException();
//        }

//        public VotedPost Convert(T from)
//        {
//            throw new NotImplementedException();
//        }

//        public List<T> Convert(ICollection<VotedPost> from)
//        {
//            throw new NotImplementedException();
//        }

//        public List<VotedPost> Convert(ICollection<T> from)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
