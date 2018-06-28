using Microsoft.AspNetCore.Identity;
using Synchronized.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface IUsersRepository: IDataRepository<ApplicationUser>, IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserRoleStore<ApplicationUser>,
        IUserSecurityStampStore<ApplicationUser>, IUserEmailStore<ApplicationUser>, IQueryableUserStore<ApplicationUser>,
        IUserLoginStore<ApplicationUser>, IUserTwoFactorStore<ApplicationUser>, IUserLockoutStore<ApplicationUser>, IUserPhoneNumberStore<ApplicationUser>
    {
        Task<List<ApplicationUser>> GetUsersPageAsync(int pageIndex, int pageSize);
    }
}
