using Synchronized.Domain;
using Synchronized.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Synchronized.Domain.Factories.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Synchronized.Repository
{
    public class TagsRepository : DataRepository<Tag>, ITagsRepository
    {
        private IDomainModelFactory _factory;

        public TagsRepository(DbContext context, ILogger<TagsRepository> logger, IDomainModelFactory factory) : base(context, logger)
        {
            _factory = factory;
        }

        public async Task<List<Tag>> GetAllTags()
        {
            return await _set.ToListAsync();
        }

        public override List<Tag> GetPage(int pageIndex, int pageSize, string sortOrder, string searchTerm)
        {
            var tags = GetBy(q => q.Id.Contains(searchTerm));

            tags = tags.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            var tagsList = tags.ToList();
            return tagsList;
        }
    }
}
