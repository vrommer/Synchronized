using Synchronized.Domain;
using Synchronized.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Synchronized.Domain.Factories.Interfaces;
using System.Threading.Tasks;

namespace Synchronized.Repository
{
    public class TagsRepository : DataRepository<Tag>, ITagsRepository
    {
        private IDomainModelFactory _factory;

        public TagsRepository(DbContext context, IDomainModelFactory factory) : base(context)
        {
            _factory = factory;
        }

        public async Task<List<Tag>> GetAllTags()
        {
            return await _set.ToListAsync();
        }

        public override List<Tag> GetPage(int pageIndex, int pageSize, string searchTerm, string sortOrder)
        {
            var tags = _set.AsNoTracking()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            //switch (sortOrder)
            //{
            //    case "Date":
            //        questions = questions.OrderBy(q => q.DatePosted.ToString());
            //        break;
            //    case "date_desc":
            //        questions = questions.OrderByDescending(q => q.DatePosted.ToString());
            //        break;
            //    case "Answers":
            //        questions = questions.OrderBy(q => q.Answers.Count);
            //        break;
            //    case "answers_desc":
            //        questions = questions.OrderByDescending(q => q.Answers.Count);
            //        break;
            //    case "Views":
            //        questions = questions.OrderBy(q => q.QuestionViews.Count);
            //        break;
            //    case "views_desc":
            //        questions = questions.OrderByDescending(q => q.QuestionViews.Count);
            //        break;
            //    case "Points":
            //        questions = questions.OrderBy(q => q.Votes.Count);
            //        break;
            //    case "points_desc":
            //        questions = questions.OrderByDescending(q => q.Votes.Count);
            //        break;
            //    default:
            //        questions = questions.OrderByDescending(q => q.Answers.Count);
            //        break;
            //}

            var tagsList = tags.ToList();
            return tagsList;
        }

        // -------------------------- Snippet - beginning of filtering questions by tags -------------------------- //
        //var QuestionTagsWithQuestions = _context.Set<QuestionTag>().AsNoTracking()
        //    .OrderBy(t => t.QuestionId)
        //    .Skip((pageIndex - 1) * pageSize)
        //    .Take(pageSize)
        //    .Include(qt => qt.Question)
        //    .ToList();

        //var questions = _factory.GetQuestionsList();
        //foreach (var qt in QuestionTagsWithQuestions)
        //{

        //}
        // -------------------------- Snippet - beginning of filtering questions by tags -------------------------- //
    }
}
