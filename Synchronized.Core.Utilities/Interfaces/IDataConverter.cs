namespace Synchronized.Core.Utilities.Interfaces
{
    public interface IDataConverter: IPostConverter, IVotedPostConverter, 
        IQuestionConverter, IAnswerConverter, ICommentConverter, 
        IUserConverter, IFlagConverter, IDeleteVoteConverter, 
        ITagsConverter
    {
    }
}
