using Synchronized.UI.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Synchronized.ServiceModel;
using Synchronized.SharedLib.Interfaces;
using Synchronized.ViewModel;
using Synchronized.ViewModel.QuestionsViewModels;

namespace Synchronized.UI.Utilities
{
    public class DataConverter : IDataConverter
    {
        public QuestionForHomePage Convert(Question from, QuestionForHomePage to)
        {
            throw new NotImplementedException();
        }

        public QuestionForQuestionsPage Convert(Question from, QuestionForQuestionsPage to)
        {
            throw new NotImplementedException();
        }

        public QuestionForDetailsPage Convert(Question from, QuestionForDetailsPage to)
        {
            throw new NotImplementedException();
        }
    }
}
