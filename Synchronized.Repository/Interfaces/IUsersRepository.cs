using Synchronized.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface IUsersRepository: IDataRepository<ApplicationUser>
    {
        Task<List<ApplicationUser>> GetUsersPageAsync(int pageIndex, int pageSize);
    }
}
