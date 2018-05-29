using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.Core.Utilities.Interfaces
{
    public interface IDataConverter: IPostConverter, IVotedPostConverter, IQuestionConverter, IAnswerConverter, ICommentConverter, IUserConverter, IFlagConverter, IDeleteVoteConverter
    {
    }
}
