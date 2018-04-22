using Synchronized.Repository.Interfaces;
using Synchronized.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Collections.Generic;
using Synchronized.Data;
using System.Linq;
using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Synchronized.Repository.Repositories
{
    public class UsersRepository : IUsersRepository, IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserRoleStore<ApplicationUser>, 
        IUserSecurityStampStore<ApplicationUser>, IUserEmailStore<ApplicationUser>, IQueryableUserStore<ApplicationUser>, 
        IUserLoginStore<ApplicationUser>, IUserTwoFactorStore<ApplicationUser>, IUserLockoutStore<ApplicationUser>, IUserPhoneNumberStore<ApplicationUser>
    {
        private readonly UserStore<ApplicationUser> _userStore = new UserStore<ApplicationUser>(new SynchronizedDbContext());

        public IQueryable<ApplicationUser> Users => ((IQueryableUserStore<ApplicationUser>)_userStore).Users;

        public void Add(ApplicationUser item)
        {
            throw new NotImplementedException();
        }

        public Task AddLoginAsync(ApplicationUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            return ((IUserLoginStore<ApplicationUser>)_userStore).AddLoginAsync(user, login, cancellationToken);
        }

        public Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            return ((IUserRoleStore<ApplicationUser>)_userStore).AddToRoleAsync(user, roleName, cancellationToken);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return ((IUserStore<ApplicationUser>)_userStore).CreateAsync(user, cancellationToken);
        }

        public void Delete(string itemId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return ((IUserStore<ApplicationUser>)_userStore).DeleteAsync(user, cancellationToken);
        }

        public void Dispose()
        {
            _userStore.Dispose();
        }

        public Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return ((IUserEmailStore<ApplicationUser>)_userStore).FindByEmailAsync(normalizedEmail, cancellationToken);
        }

        public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return ((IUserStore<ApplicationUser>)_userStore).FindByIdAsync(userId, cancellationToken);
        }

        public Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return ((IUserLoginStore<ApplicationUser>)_userStore).FindByLoginAsync(loginProvider, providerKey, cancellationToken);
        }

        public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return ((IUserStore<ApplicationUser>)_userStore).FindByNameAsync(normalizedUserName, cancellationToken);
        }

        public Task<int> GetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetAccessFailedCountAsync(user, cancellationToken);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetEmailAsync(user, cancellationToken);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetEmailConfirmedAsync(user, cancellationToken);
        }

        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetLockoutEnabledAsync(user, cancellationToken);
        }

        public Task<DateTimeOffset?> GetLockoutEndDateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetLockoutEndDateAsync(user, cancellationToken);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return ((IUserLoginStore<ApplicationUser>)_userStore).GetLoginsAsync(user, cancellationToken);
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetNormalizedEmailAsync(user, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetNormalizedUserNameAsync(user, cancellationToken);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetPasswordHashAsync(user, cancellationToken);
        }

        public Task<string> GetPhoneNumberAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetPhoneNumberAsync(user, cancellationToken);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetPhoneNumberConfirmedAsync(user, cancellationToken);
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return ((IUserRoleStore<ApplicationUser>)_userStore).GetRolesAsync(user, cancellationToken);
        }

        public Task<string> GetSecurityStampAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetSecurityStampAsync(user, cancellationToken);
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetTwoFactorEnabledAsync(user, cancellationToken);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetUserIdAsync(user, cancellationToken);
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetUserNameAsync(user, cancellationToken);
        }

        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return ((IUserRoleStore<ApplicationUser>)_userStore).GetUsersInRoleAsync(roleName, cancellationToken);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.HasPasswordAsync(user, cancellationToken);
        }

        public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.IncrementAccessFailedCountAsync(user, cancellationToken);
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            return ((IUserRoleStore<ApplicationUser>)_userStore).IsInRoleAsync(user, roleName, cancellationToken);
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            return ((IUserRoleStore<ApplicationUser>)_userStore).RemoveFromRoleAsync(user, roleName, cancellationToken);
        }

        public Task RemoveLoginAsync(ApplicationUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return ((IUserLoginStore<ApplicationUser>)_userStore).RemoveLoginAsync(user, loginProvider, providerKey, cancellationToken);
        }

        public Task ResetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.ResetAccessFailedCountAsync(user, cancellationToken);
        }

        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            return _userStore.SetEmailAsync(user, email, cancellationToken);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            return _userStore.SetEmailConfirmedAsync(user, confirmed, cancellationToken);
        }

        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            return _userStore.SetLockoutEnabledAsync(user, enabled, cancellationToken);
        }

        public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            return _userStore.SetLockoutEndDateAsync(user, lockoutEnd, cancellationToken);
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return _userStore.SetNormalizedEmailAsync(user, normalizedEmail, cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return _userStore.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            return _userStore.SetPasswordHashAsync(user, passwordHash, cancellationToken);
        }

        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            return _userStore.SetPhoneNumberAsync(user, phoneNumber, cancellationToken);
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            return _userStore.SetPhoneNumberConfirmedAsync(user, confirmed, cancellationToken);
        }

        public Task SetSecurityStampAsync(ApplicationUser user, string stamp, CancellationToken cancellationToken)
        {
            return _userStore.SetSecurityStampAsync(user, stamp, cancellationToken);
        }

        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            return _userStore.SetTwoFactorEnabledAsync(user, enabled, cancellationToken);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            return _userStore.SetUserNameAsync(user, userName, cancellationToken);
        }

        public void Update(ApplicationUser item)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return ((IUserStore<ApplicationUser>)_userStore).UpdateAsync(user, cancellationToken);
        }

        #region IDataRepository implementation
        public IQueryable<ApplicationUser> FindBy(Expression<Func<ApplicationUser, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ApplicationUser> GetPage(int pageIndex, int pageSize)
        {
            return _userStore.Users.AsNoTracking().Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    
        public ApplicationUser FindById(string userId)
        {
            return _userStore.Users.AsNoTracking().Include(u => u.Tags).SingleOrDefault(u => u.Id.Equals(userId));
        }
        #endregion

        #region IUSersRepository implementation
        public async Task<int> GetCount()
        {
            return await _userStore.Users.CountAsync();
        }

        public async Task<List<ApplicationUser>> GetUsersPageAsync(int pageIndex, int pageSize)
        {
            return await GetPage(pageIndex, pageSize).Include(u => u.Tags).ToListAsync();
        }

        public async Task AddAsync(ApplicationUser entity)
        {
            await _userStore.CreateAsync(entity);
        }
        #endregion
    }
}
