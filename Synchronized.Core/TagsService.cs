using Synchronized.Core.Interfaces;
using Synchronized.Core.Factories.Interfaces;
using Synchronized.Core.Utilities.Interfaces;
using Synchronized.Repository.Interfaces;
using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;
using System.Threading.Tasks;

namespace Synchronized.Core
{
    public class TagsService : DataService<Domain.Tag, Tag>, ITagsService
    {
        public TagsService(ITagsRepository repo, IServiceModelFactory factory, IDataConverter converter) : base(repo, factory, converter)
        {
        }

        public PaginatedList<Tag> GetTagsPage(int pageIndex, int pageSize, string sortOrder, string searchTerm)
        {
            var tags = ((ITagsRepository)_repo).GetPage(pageIndex, pageSize, sortOrder, searchTerm);
            var coreTags = _converter.Convert(tags);
            var tagsPage = _factory.GetTagsPage(coreTags, _repo.GetCount(), pageSize, pageIndex);
            return tagsPage;
        }
    }
}
