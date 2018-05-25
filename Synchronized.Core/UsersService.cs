//using Synchronized.Core.Interfaces;
//using Synchronized.Domain;
//using System;
//using System.Collections.Generic;
//using Synchronized.SharedLib.Utilities;
//using System.Threading.Tasks;
//using System.Linq.Expressions;
//using Synchronized.Repository.Interfaces;

//namespace Synchronized.Core
//{
//    public class UsersService : IUsersServiceOld
//    {
//        private readonly IUsersRepository _repo;

//        public UsersService(IUsersRepository repo)
//        {
//            _repo = repo;
//        }

//        public void Add(ApplicationUser item)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> CanVote()
//        {
//            throw new NotImplementedException();
//        }

//        public Task CreateAsync(ApplicationUser entity)
//        {
//            throw new NotImplementedException();
//        }

//        public void Delete(string itemId)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<ApplicationUser> FindBy(Expression<Func<ApplicationUser, bool>> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<ApplicationUser> FindById(string userId)
//        {
//            return await _repo.GetByIdAsync(userId);
//        }

//        public IEnumerable<ApplicationUser> GetAll()
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<PaginatedList<ApplicationUser>> GetUsersPageAsync(int pageIndex, int pageSize)
//        {
//            var users = await _repo.GetUsersPageAsync(pageIndex, pageSize);
//            int count = await _repo.GetCountAsync();
//            return new PaginatedList<ApplicationUser>(users, count, pageIndex, pageSize);
//        }

//        public void Update(ApplicationUser item)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
