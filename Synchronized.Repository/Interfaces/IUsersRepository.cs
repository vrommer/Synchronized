using Microsoft.AspNetCore.Identity;
using Synchronized.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    /// <summary>
    /// This interface is for workng with users in the Database. In edition to representing a Users Repository, this class also represents a UserManager 
    /// by implementing the corresponding interfaces of Microsoft.AspNetCore.Identity. 
    /// </summary>
    public interface IUsersRepository: IDataRepository<ApplicationUser>, IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserRoleStore<ApplicationUser>,
        IUserSecurityStampStore<ApplicationUser>, IUserEmailStore<ApplicationUser>, IQueryableUserStore<ApplicationUser>,
        IUserLoginStore<ApplicationUser>, IUserTwoFactorStore<ApplicationUser>, IUserLockoutStore<ApplicationUser>, IUserPhoneNumberStore<ApplicationUser>
    {
        /// <summary>
        /// Get a page of Users.
        /// </summary>
        /// <param name="pageIndex">The index of the page.</param>
        /// <param name="pageSize">the size of the page.</param>
        /// <returns>A List of ApplicationUsers</returns>
        Task<List<ApplicationUser>> GetUsersPageAsync(int pageIndex, int pageSize);
    }
}
