using Synchronized.Domain;
using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IUsersServiceOld : IDataServiceOld<User, ApplicationUser>
    {
        Task<PaginatedList<ApplicationUser>> GetUsersPageAsync(int currentPage, int pageSize);
        Task<bool> CanVote();
    }
}
