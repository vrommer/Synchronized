using Synchronized.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface IUsersRepository: IDataRepositoryOld<ApplicationUser>
    {
        Task<List<ApplicationUser>> GetUsersPageAsync(int pageIndex, int pageSize);
    }
}
