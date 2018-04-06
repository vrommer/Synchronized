using Synchronized.Model;
using Synchronized.Repository.Interfaces;
using Synchronized.Repository.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Synchronized.Repository
{
    public class QuestionsRepository : DataRepository<Question>, IQuestionsRepository
    {
        protected DbSet<QuestionTag> _questionTags;

        public QuestionsRepository(DbContext context) : base(context)
        {
            _questionTags = context.Set<QuestionTag>();
        }

        public async Task<List<Question>> GetQuestionsPageWithTagsAsync(int pageIndex, int pageSize)
        {
            var source = _dbSet.AsNoTracking()
                .Include(q => q.QuestionTags);

            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            items.ForEach(i => i.QuestionTags = _questionTags.AsNoTracking().Where(t => t.QuestionId == i.Id).Include(qt => qt.Question).Include(qt => qt.Tag).ToList() as ICollection<QuestionTag>);
            return items;
        }
    }
}
