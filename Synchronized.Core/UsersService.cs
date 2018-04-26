using Synchronized.Core.Interfaces;
using Synchronized.Model;
using System;
using System.Collections.Generic;
using Synchronized.SharedLib.Utilities;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Synchronized.Repository.Interfaces;

namespace Synchronized.Core
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repo;

        public UsersService(IUsersRepository repo)
        {
            _repo = repo;
        }

        public void Add(ApplicationUser item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CanVote()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string itemId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> FindBy(Expression<Func<ApplicationUser, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser FindById(string userId)
        {
            return _repo.FindById(userId);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedList<ApplicationUser>> GetUsersPageAsync(int pageIndex, int pageSize)
        {
            var users = await _repo.GetUsersPageAsync(pageIndex, pageSize);
            int count = await _repo.GetCount();
            return new PaginatedList<ApplicationUser>(users, count, pageIndex, pageSize);
        }

        public void Update(ApplicationUser item)
        {
            throw new NotImplementedException();
        }
    }
}
