using System.Collections.Generic;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel;
using Synchronized.ViewModel.QuestionsViewModels;
using System;
using Synchronized.ViewModel.TagsViewModels;
using Synchronized.ViewModel.UsersViewModels;

namespace Synchronized.ViewModelFactories.Interfaces
{
    public interface IViewModelFactory
    {
        // Generic method for creating any kind of object
        T GetOfType<T>();

        QuestionForHomePage GetQuestionForHomePage();
        QuestionForQuestionsPage GetQuestionForQuestionsPage();
        QuestionForDetailsPage GetQuestionForDetailsPage();
        List<QuestionForHomePage> GetHomePageQuestionsList();
        PaginatedList<T> GetPaginatedList<T>(int count, int pageIndex, int pageSize);
        AnswerViewModel GetAnswer();
        CommentViewModel GetComment();
        List<AnswerViewModel> GetAnswers();
        List<CommentViewModel> GetComments();
        UserViewModel GetUser();
        TagViewModel GetTag();
        List<TagViewModel> GetTags();
        EditViewModel GetEditViewModel();
    }
}
