using System.Collections.Generic;

namespace Synchronized.ViewModel.QuestionsViewModels
{
    /// <summary>
    /// This Class represents a Question in the Quiestion Details View Context.
    /// </summary>
    public class QuestionForDetailsPage: QuestionForQuestionsPage
    {
        public ICollection<AnswerViewModel> Answers { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }
    }
}
