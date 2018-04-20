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

        public List<Question> GetQuestionsPageWithUsersAsync(int pageIndex, int pageSize)
        {
            var questions = GetQuestionsQueryWithUsers(pageIndex, pageSize).ToList();
            AddTagsToQuestions(questions);
            return questions;
        }

        public List<Question> GetQuestionsPageWithUsersAsync(int pageIndex, int pageSize, string sortOrder)
        {
            var questions = GetQuestionsQueryWithUsers(pageIndex, pageSize);          

            switch (sortOrder)
            {
                case "Date":
                    questions = questions.OrderBy(q => q.DatePosted);
                    break;
                case "date_desc":
                    questions = questions.OrderByDescending(q => q.DatePosted);
                    break;
                case "Answers":
                    questions = questions.OrderBy(q => q.Answers.Count);
                    break;
                case "answers_desc":
                    questions = questions.OrderByDescending(q => q.Answers.Count);
                    break;
                case "Views":
                    questions = questions.OrderBy(q => q.Views);
                    break;
                case "views_desc":
                    questions = questions.OrderByDescending(q => q.Views);
                    break;
                case "Points":
                    questions = questions.OrderBy(q => q.Points);
                    break;
                case "points_desc":
                    questions = questions.OrderByDescending(q => q.Points);
                    break;
                default:
                    questions = questions.OrderByDescending(q => q.Answers.Count);
                    break;
            }

            var questionsList = questions.ToList();
            AddTagsToQuestions(questionsList);
            return questionsList;
        }

        public async Task<List<Question>> GetQuestionsPageAsync(int pageIndex, int pageSize)
        {
            var questions = await GetQuestionsQuery(pageIndex, pageSize).ToListAsync();
            AddTagsToQuestions(questions);
            return questions;
        }

        private IQueryable<Question> GetQuestionsQuery(int pageIndex, int pageSize)
        {
            return GetPage(pageIndex, pageSize)
                .Include(q => q.QuestionTags)
                .Include(q => q.Answers);
        }

        private IQueryable<Question> GetQuestionsQueryWithUsers(int pageIndex, int pageSize)
        {
            //return GetPage(pageIndex, pageSize)
            //    .Include(q => q.QuestionTags)
            //    .Include(q => q.Publisher)
            //    .Include(q => q.Answers);

            return GetQuestionsQuery(pageIndex, pageSize).Include(q => q.Publisher);
        }

        private void AddTagsToQuestions(List<Question> questions)
        {
            questions.ForEach(q => q.QuestionTags = _questionTags.AsNoTracking().Where(t => t.QuestionId == q.Id).Include(qt => qt.Question).Include(qt => qt.Tag).ToList() as ICollection<QuestionTag>);
        }

        public Question FindQuestionById(string questionId)
        {
            return _dbSet.Include(q => q.Answers)
                .Include(q => q.Comments)
                .Include(q => q.Publisher)
                .SingleOrDefault(e => e.Id.Equals(questionId));
        }
    }
}
