using Synchronized.ViewModel.QuestionsViewModels;
using System.Collections.Generic;

namespace Synchronized.ViewModel.QuestionsViewModels
{
    public class QuestionForDetailsPage: QuestionForQuestionsPage
    {
        public ICollection<AnswerViewModel> Answers { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }
    }
}
