using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Synchronized.Data;
using Synchronized.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronized.Core.Repositories
{
    public class RolesRepository: IRoleStore<IdentityRole>
    {
        private readonly RoleStore<IdentityRole> _roleStore/* = new RoleStore<IdentityRole>(new SynchronizedDbContext())*/;

        public RolesRepository(DbContext context)
        {
            _roleStore = new RoleStore<IdentityRole>(context);
        }

        public Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return _roleStore.CreateAsync(role, cancellationToken);
        }

        public Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return _roleStore.DeleteAsync(role, cancellationToken);
        }

        public void Dispose()
        {
            _roleStore.Dispose();
        }

        public Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return _roleStore.FindByIdAsync(roleId, cancellationToken);
        }

        public Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return _roleStore.FindByNameAsync(normalizedRoleName, cancellationToken);
        }

        public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return _roleStore.GetNormalizedRoleNameAsync(role, cancellationToken);
        }

        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return _roleStore.GetRoleIdAsync(role, cancellationToken);
        }

        public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return _roleStore.GetRoleNameAsync(role, cancellationToken);
        }

        public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {
            return _roleStore.SetNormalizedRoleNameAsync(role, normalizedName, cancellationToken);
        }

        public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
        {
            return _roleStore.SetRoleNameAsync(role, roleName, cancellationToken);
        }

        public Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return _roleStore.UpdateAsync(role, cancellationToken);
        }
    }
}
