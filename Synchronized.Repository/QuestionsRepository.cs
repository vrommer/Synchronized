using Synchronized.Repository.Interfaces;
using System.Collections.Generic;
using Synchronized.Domain;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Synchronized.Repository
{
    public class QuestionsRepository : PostsRepository<Question>,  IQuestionsRepository
    {
        private DbSet<Tag> _tagsSet;

        public QuestionsRepository(DbContext context): base(context)
        {
            _tagsSet = context.Set<Tag>();
        }

        public async Task<List<Question>> GetPageAsync(int pageIndex, int pageSize)
        {
            var questions = await _set.AsNoTracking()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Include(q => q.Answers)
                .Include(q => q.QuestionTags)
                    .ThenInclude(qt => qt.Tag)
                .Include(q => q.Votes)
                .ToListAsync();

            return questions;
        }

        public override List<Question> GetPage(int pageIndex, int pageSize, string sortOrder, string searchString)
        {
            var questions = GetBy(q => q.Title.Contains(searchString) || q.Body.Contains(searchString));

            switch (sortOrder)
            {
                case "Date":
                    questions = questions.OrderBy(q => q.DatePosted.ToString());
                    break;
                case "date_desc":
                    questions = questions.OrderByDescending(q => q.DatePosted.ToString());
                    break;
                case "Answers":
                    questions = questions.OrderBy(q => q.Answers.Count);
                    break;
                case "answers_desc":
                    questions = questions.OrderByDescending(q => q.Answers.Count);
                    break;
                case "Views":
                    questions = questions.OrderBy(q => q.QuestionViews.Count);
                    break;
                case "views_desc":
                    questions = questions.OrderByDescending(q => q.QuestionViews.Count);
                    break;
                case "Points":
                    questions = questions.OrderBy(q => q.Votes.Count);
                    break;
                case "points_desc":
                    questions = questions.OrderByDescending(q => q.Votes.Count);
                    break;
                default:
                    questions = questions.OrderByDescending(q => q.Answers.Count);
                    break;
            }

            questions = questions.AsNoTracking()
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Include(q => q.Votes)
                .Include(q => q.QuestionViews)
                .Include(q => q.QuestionTags)
                    .ThenInclude(qt => qt.Tag)
                .Include(q => q.Publisher)
                .Include(q => q.Answers);

            var questionsList = questions.ToList();
            return questionsList;
        }

        public async Task<Question> GetQuestionById(string id)
        {
            var question = await _set
                .AsNoTracking()
                .Include(q => q.Answers)
                    .ThenInclude(a => a.Publisher)
                .Include(q => q.Answers)
                    .ThenInclude(a => a.Votes)
                .Include(q => q.Comments)
                .Include(q => q.Publisher)
                .Include(q => q.PostFlags)
                .Include(q => q.DeleteVotes)
                .Include(q => q.Votes)
                .Include(q => q.QuestionViews)
                .Include(q => q.Subscriptions)
                    .ThenInclude(s => s.Subscriber)
                .Include(q => q.QuestionTags)
                    .ThenInclude(qt => qt.Tag)
                .SingleOrDefaultAsync(e => e.Id.Equals(id));

            // Add sorted comments to question           
            question.Comments = await _context.Set<Comment>().Where(c => c.PostId == question.Id)
                .OrderBy(c => c.DatePosted)
                .Include(c => c.Publisher)
                .ToListAsync();

            //question.Subscriptions = await _context.Set<Subscription>()
            //    .Where(s => s.QuestionId.Equals(question.Id))
            //    .Include(s => s.Subscriber)
            //    .ToListAsync();

            // Add sorted comments to each answer
            foreach (Answer a in question.Answers)
            {
                a.Comments = _context.Set<Comment>()
                    .Where(c => c.PostId == a.Id)
                    .Include(c => c.Publisher)
                    .OrderBy(c => c.DatePosted).ToList();
            }

            return question;
        }

        public async Task<Answer> GetAnswerById(string postId)
        {
            var answer = await _context.Set<Answer>().Where(a => a.Id == postId)
                .Include(a => a.Question)
                .Include(a => a.Votes)
                .Include(a => a.Comments)
                .Include(a => a.DeleteVotes)
                .Include(a => a.Publisher)
                .Include(a => a.Comments)
                    .ThenInclude(c => c.Publisher)
                .SingleOrDefaultAsync();


            answer.Question.Subscriptions = await _context.Set<Subscription>()
                .Where(s => s.QuestionId.Equals(answer.Question.Id))
                .Include(s => s.Subscriber)
                .ToListAsync();

            answer.Comments.ToList();
            return answer;
        }

        public async Task UpdateAnswerAsync(Answer answer)
        {
            _context.Set<Answer>().Update(answer);
            await _context.SaveChangesAsync();
        }

        public async Task<Comment> GetCommentById(string commentId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Tag> GetQuestionTagById(string tagId)
        {
            return await _tagsSet.FindAsync(tagId);
        }
    }
}
