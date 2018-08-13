using Synchronized.ViewModel.QuestionsViewModels;
using System.Collections.Generic;

namespace Synchronized.ViewModel
{
    public class HomeViewModel
    {
        /// <summary>
        /// This class contains data to be presented in the home view.
        /// </summary>
        public List<QuestionForHomePage> Questions { get; set; }
    }
}