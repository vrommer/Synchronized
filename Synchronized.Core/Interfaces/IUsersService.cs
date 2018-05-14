using Synchronized.Model;
using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IUsersService : IDataService<User, ApplicationUser>
    {
        Task<PaginatedList<ApplicationUser>> GetUsersPageAsync(int currentPage, int pageSize);
        Task<bool> CanVote();
    }
}
