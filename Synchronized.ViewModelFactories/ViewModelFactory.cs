using System.Collections.Generic;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel;
using Synchronized.ViewModel.QuestionsViewModels;
using Synchronized.ViewModelFactories.Interfaces;
using System;

namespace Synchronized.ViewModelFactories
{
    public class ViewModelFactory: IViewModelFactory
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

        public List<QuestionForHomePage> GetHomePageQuestionsList()
        {
            throw new NotImplementedException();
        }

        public PaginatedList<T> GetPaginatedList<T>(int count, int pageIndex, int pageSize)
        {
            return new PaginatedList<T>(count, pageIndex, pageSize);            
        }

        public AnswerViewModel GetAnswer()
        {
            return new AnswerViewModel();
        }

        public CommentViewModel GetComment()
        {
            return new CommentViewModel();
        }

        public List<AnswerViewModel> GetAnswers()
        {
            return new List<AnswerViewModel>();
        }

        public List<CommentViewModel> GetComments()
        {
            return new List<CommentViewModel>();
        }
    }
}
