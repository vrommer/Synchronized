using Synchronized.Repository.Interfaces;
using System;
using System.Collections.Generic;
using Synchronized.Domain;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Synchronized.Repository
{
    public class QuestionsRepository : PostsRepository,  IQuestionsRepository
    {

        public QuestionsRepository(DbContext context): base(context)
        {

        }

        public Task AddAsync(Question entity)
        {
            throw new NotImplementedException();
        }

        public Task<Question> GetBy(Expression<Func<Question, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Question>> GetPageAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Question Entity)
        {
            throw new NotImplementedException();
        }
    }
}
