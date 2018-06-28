using Synchronized.Domain;
using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;
using System;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IUsersService : IDataService<User>
    {
        PaginatedList<User> GetUsersPage(int pageIndex, int pageSize, string sortOrder, string searchTerm);
        Task UpdateUserRoles(String userId);
        Task UpdateUserRoles(ApplicationUser user);
    }
}
