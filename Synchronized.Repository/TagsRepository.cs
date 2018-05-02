using Synchronized.Model;
using Synchronized.Repository.Interfaces;
using Synchronized.Repository.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Synchronized.Repository
{
    public class TagsRepository : DataRepository<Tag>, ITagsRepository
    {
        public TagsRepository(DbContext context) : base(context)
        {
        }

        public async Task<Tag> FindTagByName(string name)
        {
            return await FindBy(t => t.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<List<Tag>> GetTagsPageAsync(int pageIndex, int pageSize)
        {
            return await GetPage(pageIndex, pageSize)
                .Include(t => t.Publisher)
                .ToListAsync();
        }
    }
}
