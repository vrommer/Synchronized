using Synchronized.Repository.Interfaces;
using System.Collections.Generic;
using Synchronized.Domain;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

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
                .Where(q => q.DeleteVotes.Count < 3)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Include(q => q.Answers)
                .Include(q => q.QuestionTags)
                    .ThenInclude(qt => qt.Tag)
                .Include(q => q.Votes)
                .ToListAsync();

            return questions;
        }

        public override List<Question> GetPage(int pageIndex, int pageSize, string searchString, string sortOrder)
        {
            var questions = GetBy(q => q.Title.Contains(searchString) || q.Body.Contains(searchString));
            questions = questions.Where(q => q.DeleteVotes.Count < 3);

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
                //.Include(q => q.Answers)
                //    .ThenInclude(a => a.Votes)
                //.Include(q => q.Comments)
                //.Include(q => q.Publisher)
                .Include(q => q.PostFlags)
                .Include(q => q.DeleteVotes)
                .Include(q => q.Votes)
                .Include(q => q.QuestionViews)
                .Include(q => q.Subscriptions)
                    .ThenInclude(s => s.Subscriber)
                .Include(q => q.QuestionTags)
                    .ThenInclude(qt => qt.Tag)
                .SingleOrDefaultAsync(e => e.Id.Equals(id));

            question.Publisher = await _context.Set<ApplicationUser>()
                .AsNoTracking()
                .Where(u => u.Id.Equals(question.PublisherId))
                .SingleOrDefaultAsync();

            // Add sorted comments to question           
            question.Comments = await _context.Set<Comment>()
                .AsNoTracking()
                .Where(c => c.PostId == question.Id)
                .OrderBy(c => c.DatePosted)
                .Include(c => c.Publisher)
                .ToListAsync();

            question.Answers = await _context.Set<Answer>()
                .AsNoTracking()
                .Where(a => a.QuestionId.Equals(question.Id) && a.DeleteVotes.Count < 3)
                .Include(a => a.Question)
                .Include(a => a.Publisher)
                .Include(a => a.Votes)
                .ToListAsync();

            foreach (Answer a in question.Answers)
            {
                a.Comments = _context.Set<Comment>()
                    .AsNoTracking()
                    .Where(c => c.PostId == a.Id)
                    .Include(c => c.Publisher)
                    .OrderBy(c => c.DatePosted).ToList();
            }

            return question;
        }

        public async Task<Answer> GetAnswerById(string postId)
        {
            var answer = await _context.Set<Answer>()
                .Where(a => a.Id == postId && a.DeleteVotes.Count < 3)
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
            throw new NotImplementedException();
        }

        public async Task<Tag> GetQuestionTagById(string tagId)
        {
            return await _tagsSet.FindAsync(tagId);
        }

        public async Task<HashSet<Question>> GetReviewPage(int pageIndex, int pageSize)
        {
            IQueryable<Question> questionQueryable;
            Question question;
            HashSet<Question> returnList = new HashSet<Question>();
            var votedPosts = await _context.Set<VotedPost>()
                .Where(p => p.Review)
                .OrderBy(q => q.ReviewDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            foreach(VotedPost p in votedPosts)
            {
                if (p.GetType().Equals(typeof(Question)))
                {
                    questionQueryable = _set
                        .Where(q => q.Id.Equals(p.Id));
                }
                else
                {
                    questionQueryable = _set
                        .Where(q => q.Id.Equals(((Answer)p).QuestionId));
                }
                question = await questionQueryable
                        .Include(q => q.Votes)
                        .Include(q => q.QuestionViews)
                        .Include(q => q.QuestionTags)
                            .ThenInclude(qt => qt.Tag)
                        .Include(q => q.Publisher)
                        .Include(q => q.Answers)
                        .SingleOrDefaultAsync();
                if (!returnList.Contains(question))
                {
                    returnList.Add(question);
                }
            }

            return returnList;
        }

        public int GetReviewCount()
        {
            return GetBy(q => q.PostFlags.Count > 0).Count();
        }
    }
}
