namespace Synchronized.UI.Utilities.Interfaces
{
    /// <summary>
    /// This interface provides functions for converting types of ServiceModel Posts to types of ViewModel.
    /// </summary>
    public interface IPostsConverter: IHomeViewConverter, IQuestionsConverter, IDetailsConverter, 
        IAnswerConverter, ICommentConverter, IAskConverter, IEditConverter
    {
    }
}