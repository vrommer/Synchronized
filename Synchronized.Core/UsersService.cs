using System.Threading.Tasks;
using Synchronized.Core.Factories.Interfaces;
using Synchronized.Core.Interfaces;
using Synchronized.Core.Utilities.Interfaces;
using Synchronized.Repository.Interfaces;
using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;

namespace Synchronized.Core
{
    public class UsersService : DataService<Domain.ApplicationUser, User>, IUsersService
    {
        public UsersService(IUsersRepository repo, IServiceModelFactory factory, IDataConverter converter) : base(repo, factory, converter)
        {
        }

        public async override Task<User> GetById(string id)
        {
            var user = await _repo.GetById(id);
            var coreUser = _converter.Convert(user);
            return coreUser;
        }

        public PaginatedList<User> GetUsersPage(int pageIndex, int pageSize, string sortOrder, string searchTerm)
        {
            var users = ((IUsersRepository)_repo).GetPage(pageIndex, pageSize, sortOrder, searchTerm);
            var coreUsers = _converter.Convert(users);
            var usersPage = _factory.GetUsersPage(coreUsers, _repo.GetCount(), pageSize, pageIndex);
            return usersPage;
        }
    }
}
