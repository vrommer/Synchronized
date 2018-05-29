using System.Collections.Generic;
using Synchronized.ViewModel;
using Synchronized.ViewModel.QuestionsViewModel;
using Synchronized.ViewModel.QuestionsViewModels;

namespace Syncrhonized.ViewModelFactories
{
    public class ViewModelFactory
    {
        public QuestionForHomePage GetQuestionForHomePage()
        {
            var question = new QuestionForHomePage();

            return question;
        }

        public QuestionForQuestionsPage GetQuestionForQuestionsPage()
        {
            var question = new QuestionForQuestionsPage();

            return question;
        }

        public QuestionForDetailsPage GetQuestionForDetailsPage()
        {
            var question = new QuestionForDetailsPage
            {
                Answers = new List<AnswerViewModel>(),
                Comments = new List<CommentViewModel>()
            };
            foreach(var q in question.Answers)
            {
                q.Comments = new List<CommentViewModel>();
            }

            return question;
        }
    }
}
