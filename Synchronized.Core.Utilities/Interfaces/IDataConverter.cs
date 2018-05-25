using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.Core.Utilities.Interfaces
{
    public interface IDataConverter: IPostsConverter, IVotedPostConverter, IQuestionsConverter, IAnswersConverter, ICommentsConverter
    {
    }
}
