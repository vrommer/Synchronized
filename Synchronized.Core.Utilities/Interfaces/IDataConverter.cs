namespace Synchronized.Core.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for Converter Objects of Domain.Model to Objects of ServiceModel and from Objects of ServiceModel to Objects of Domain.Model.
    /// </summary>
    public interface IDataConverter: IPostConverter, IVotedPostConverter, 
        IQuestionConverter, IAnswerConverter, ICommentConverter, 
        IUserConverter, IFlagConverter, IDeleteVoteConverter, 
        ITagsConverter
    {
    }
}
