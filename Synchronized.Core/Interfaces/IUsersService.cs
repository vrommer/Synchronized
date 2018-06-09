using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;

namespace Synchronized.Core.Interfaces
{
    public interface IUsersService : IDataService<User>
    {
        PaginatedList<User> GetUsersPage(int pageIndex, int pageSize, string sortOrder, string searchTerm);
    }
}
