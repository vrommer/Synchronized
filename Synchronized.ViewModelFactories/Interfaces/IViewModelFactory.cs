using System.Collections.Generic;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel;
using Synchronized.ViewModel.QuestionsViewModels;
using Synchronized.ViewModel.TagsViewModels;
using Synchronized.ViewModel.UsersViewModels;

namespace Synchronized.ViewModelFactories.Interfaces
{
    /// <summary>
    /// This interface defines methods for creating Objects of the ViewModel.
    /// </summary>
    public interface IViewModelFactory
    {
        /// <summary>
        /// A generic method for Creating any kind of Data.
        /// </summary>
        /// <typeparam name="T">The required Data Type.</typeparam>
        /// <returns>A new instance of T.</returns>
        T GetOfType<T>();

        /// <summary>
        /// A method for creating instance of QuestionForHomePage.
        /// </summary>
        /// <returns>A new instance of QuestionForHomePage.</returns>
        QuestionForHomePage GetQuestionForHomePage();

        /// <summary>
        /// A method for creating instance of QuestionForQuestionsPage.
        /// </summary>
        /// <returns>A new instance of QuestionForQuestionsPage.</returns>
        QuestionForQuestionsPage GetQuestionForQuestionsPage();

        /// <summary>
        /// A method for creating instance of QuestionForDetailsPage.
        /// </summary>
        /// <returns>A new instance of QuestionForDetailsPage.</returns>
        QuestionForDetailsPage GetQuestionForDetailsPage();

        /// <summary>
        /// A method for creating instance of List of QuestionForHomePage.
        /// </summary>
        /// <returns>A new instance of List of QuestionForHomePage.</returns>
        List<QuestionForHomePage> GetHomePageQuestionsList();

        /// <summary>
        /// A generic method for creating instance of PaginatedList of T.
        /// </summary>
        /// <returns>A new instance of PaginatedList of T.</returns>
        PaginatedList<T> GetPaginatedList<T>(int count, int pageIndex, int pageSize);

        /// A method for creating instance of AnswerViewModel.
        /// </summary>
        /// <returns>A new instance of AnswerViewModel.</returns>
        AnswerViewModel GetAnswer();

        /// A method for creating instance of CommentViewModel.
        /// </summary>
        /// <returns>A new instance of CommentViewModel.</returns>
        CommentViewModel GetComment();

        /// A method for creating instance of List of AnswerViewModel.
        /// </summary>
        /// <returns>A new instance of List of AnswerViewModel.</returns>
        List<AnswerViewModel> GetAnswers();

        /// A method for creating instance of List of CommentViewModel.
        /// </summary>
        /// <returns>A new instance of List of CommentViewModel.</returns>
        List<CommentViewModel> GetComments();

        /// A method for creating instance of UserViewModel.
        /// </summary>
        /// <returns>A new instance of UserViewModel.</returns>
        UserViewModel GetUser();

        /// A method for creating instance of TagViewModel.
        /// </summary>
        /// <returns>A new instance of TagViewModel.</returns>
        TagViewModel GetTag();

        /// A method for creating instance of List of TagViewModel.
        /// </summary>
        /// <returns>A new instance of List of TagViewModel.</returns>
        List<TagViewModel> GetTags();

        /// A method for creating instance of EditViewModel.
        /// </summary>
        /// <returns>A new instance of EditViewModel.</returns>
        EditViewModel GetEditViewModel();
    }
}
