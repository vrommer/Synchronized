using Synchronized.Core.Interfaces;
using Synchronized.Core.Factories.Interfaces;
using Synchronized.Core.Utilities.Interfaces;
using Synchronized.Repository.Interfaces;
using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Synchronized.SharedLib;

namespace Synchronized.Core
{
    public class TagsService : DataService<Domain.Tag, Tag>, ITagsService
    {
        private IUsersRepository _usersRepo;

        public TagsService(ITagsRepository repo, IServiceModelFactory factory, IDataConverter converter, ILogger<TagsService> logger,
            IUsersRepository usersRepo) : base(repo, factory, converter, logger)
        {
            _usersRepo = usersRepo;
        }

        public async Task<string> CreateTag(Tag tag, int userPoints)
        {
            var canCreateTag = (Constants.CREATE_TAGS_POINTS <= userPoints);
            string status = "";
            if (canCreateTag)
            {
                status = await _repo.AddAsync(_converter.Convert(tag));
            }
            return status;
        }

        public async Task<List<Tag>> GetAllTags()
        {
            var domainTags = await ((ITagsRepository)_repo).GetAllTags();
            var servceModelTags = _converter.Convert(domainTags);
            return servceModelTags;
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
