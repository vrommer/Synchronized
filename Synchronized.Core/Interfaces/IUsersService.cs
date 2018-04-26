using Synchronized.Model;
using Synchronized.SharedLib.Utilities;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IUsersService : IDataService<ApplicationUser>
    {
        Task<PaginatedList<ApplicationUser>> GetUsersPageAsync(int currentPage, int pageSize);
        Task<bool> CanVote();
    }
}
