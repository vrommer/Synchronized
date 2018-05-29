//using Synchronized.Core.Interfaces;
//using Synchronized.ServiceModel;
//using Synchronized.ViewModel.QuestionsViewModel;
//using System.Collections.Generic;
//using System.Linq;

//namespace Synchronized.ViewServices.Factories
//{
//    /// <summary>
//    /// This class is for createing ViewModel objects
//    /// </summary>
//    public class ViewModelFactory
//    {
//        private readonly IPostsServiceOld _service;

//        public ViewModelFactory(IPostsServiceOld service)
//        {
//            _service = service;
//        }

//        public DetailsViewModel GetDetailsModel()
//        {
//            return null;
//        }

//        public IndexViewModel GetQuestionsIndexModel()
//        {
//            var serviceModelQuesitons =_service.GetPostsPage<Question>();
//            return null;
//        }
//    }
//}
