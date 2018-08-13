using Synchronized.SharedLib.Interfaces;
using System.Collections.Generic;

namespace Synchronized.Core.Utilities.Interfaces
{
    /// <summary>
    /// This interface defines methods for converting from Domain.Tag to ServiceModel.Tag and from ServiceModel.Tag to Domain.Tag.
    /// </summary>
    public interface ITagsConverter: IDataConverter<Domain.Tag, ServiceModel.Tag>
    {
        List<Domain.QuestionTag> Convert(string tags);
        string Convert(List<Domain.QuestionTag> tags);
    }
}
