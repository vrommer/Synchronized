using Synchronized.Model;
using Synchronized.Repository.Interfaces;
using Synchronized.Repository.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Synchronized.Repository
{
    public class QuestionsRepository : DataRepository<Question>, IQuestionsRepository
    {
        protected DbSet<QuestionTag> _questionTags;
        protected DbSet<Comment> _comments;

        public QuestionsRepository(DbContext context) : base(context)
        {
            _questionTags = context.Set<QuestionTag>();
            _comments = context.Set<Comment>();
        }

        public List<Question> GetQuestionsPageWithUsersAsync(int pageIndex, int pageSize)
        {
            var questions = GetQuestionsQueryWithUsers(pageIndex, pageSize).ToList();
            return questions;
        }

        public List<Question> GetQuestionsPageWithUsersAsync(int pageIndex, int pageSize, string sortOrder, string filter)
        {
            var questions = FindBy(q => q.Title.Contains(filter) || q.Body.Contains(filter));

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
                    questions = questions.OrderBy(q => q.Points);
                    break;
                case "points_desc":
                    questions = questions.OrderByDescending(q => q.Points);
                    break;
                default:
                    questions = questions.OrderByDescending(q => q.Answers.Count);
                    break;
            }

            questions = questions.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Include(q => q.QuestionViews)
                .Include(q => q.QuestionTags)
                    .ThenInclude(qt => qt.Tag)
                .Include(q => q.Publisher)
                .Include(q => q.Answers);

            var questionsList = questions.ToList();
            return questionsList;
        }

        public async Task<List<Question>> GetQuestionsPageAsync(int pageIndex, int pageSize)
        {
            var questions = await GetQuestionsQuery(pageIndex, pageSize).ToListAsync();
            return questions;
        }

        public Question FindQuestionById(string questionId)
        {
            var question = _dbSet
                .Include(q => q.Answers)
                    .ThenInclude(a => a.Publisher)
                .Include(q => q.Publisher)
                .Include(q => q.QuestionViews)
                .Include(q => q.QuestionFlags)
                .Include(q => q.DeleteVotes)
                .AsNoTracking()
                .SingleOrDefault(e => e.Id.Equals(questionId));

            // Add sorted comments to question           
            question.Comments = _context.Set<Comment>().Where(c => c.PostId == question.Id).OrderBy(c => c.DatePosted).ToList();

            // Add sorted comments to each answer
            foreach (Answer a in question.Answers)
            {
                a.Comments = _context.Set<Comment>()
                    .Where(c => c.PostId == a.Id)
                    .OrderBy(c => c.DatePosted).ToList();
            }
            return question;
        }

        public Answer FindAnswerById(string answerId)
        {
            var answer = _context.Set<Answer>()
                .Include(a => a.Comments)
                .SingleOrDefault(a => a.Id.Equals(answerId));

            return answer;
        }

        private IQueryable<Question> GetQuestionsQuery(int pageIndex, int pageSize)
        {
            return GetPage(pageIndex, pageSize)
                .Include(q => q.Answers)
                .Include(q => q.QuestionTags)
                    .ThenInclude(qt => qt.Tag);
        }

        private IQueryable<Question> GetQuestionsQueryWithUsers(int pageIndex, int pageSize)
        {
            return GetQuestionsQuery(pageIndex, pageSize)
                .Include(q => q.Publisher);
        }

        public void UpdateQuestion(Question question)
        {
            _dbSet.Attach(question);
            _context.Entry(question).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateAnswer(Answer answer)
        {
            _context.Set<Answer>().Attach(answer);
            _context.Entry(answer).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
