using Synchronized.SharedLib.Interfaces;
using System.Collections.Generic;

namespace Synchronized.Core.Utilities.Interfaces
{
    public interface ITagsConverter: IDataConverter<Domain.Tag, ServiceModel.Tag>
    {
        List<Domain.QuestionTag> Convert(string tags);
        string Convert(List<Domain.QuestionTag> tags);
    }
}
