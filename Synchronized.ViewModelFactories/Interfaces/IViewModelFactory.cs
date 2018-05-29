using System.Collections.Generic;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel;
using Synchronized.ViewModel.QuestionsViewModels;
using System;

namespace Synchronized.ViewModelFactories.Interfaces
{
    public interface IViewModelFactory
    {
        QuestionForHomePage GetQuestionForHomePage();

        QuestionForQuestionsPage GetQuestionForQuestionsPage();

        QuestionForDetailsPage GetQuestionForDetailsPage();
        List<QuestionForHomePage> GetHomePageQuestionsList();
        PaginatedList<T> GetPaginatedList<T>(int count, int pageIndex, int pageSize);
        AnswerViewModel GetAnswer();
        CommentViewModel GetComment();
        List<AnswerViewModel> GetAnswers();
        List<CommentViewModel> GetComments();
    }
}
