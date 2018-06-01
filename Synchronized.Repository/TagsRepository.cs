using Synchronized.Domain;
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

        public Task<Tag> FindTagByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Tag>> GetTagsPageAsync(int currentPage, int pageSize)
        {
            throw new System.NotImplementedException();
        }
    }
}
