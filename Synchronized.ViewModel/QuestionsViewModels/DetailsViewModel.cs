using Synchronized.ServiceModel;
using Synchronized.ViewModel.QuestionsViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Synchronized.ViewModel.QuestionsViewModels
{
    public class DetailsViewModel
    {
        public QuestionForDetailsPage Question{ get; set; }
        public Dictionary<string, AnswerViewModel> Answers{ get; set; }
        public Dictionary<string, CommentViewModel> Comments{ get; set; }
    }
}
