using System.Threading.Tasks;
using Synchronized.Core.Factories.Interfaces;
using Synchronized.Core.Interfaces;
using Synchronized.Core.Utilities.Interfaces;
using Synchronized.Repository.Interfaces;
using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;
using Microsoft.Extensions.Logging;
using Synchronized.Domain;
using Microsoft.AspNetCore.Identity;
using Synchronized.SharedLib;
using System;
using System.Threading;

namespace Synchronized.Core
{
    public class UsersService : DataService<ApplicationUser, User>, IUsersService
    {
        private UserManager<ApplicationUser> _userManager;

        public UsersService(IUsersRepository repo, UserManager<ApplicationUser> userManager, IServiceModelFactory factory, IDataConverter converter, ILogger<UsersService> logger) : base(repo, factory, converter, logger)
        {
            _userManager = userManager;
        }

        public async override Task<User> GetById(string id)
        {
            var user = await _repo.GetById(id);
            var coreUser = _converter.Convert(user);  
            coreUser.Roles = (System.Collections.Generic.List<string>)await ((IUsersRepository)_repo).GetRolesAsync(user, new CancellationToken());
            return coreUser;
        }

        public async Task<PaginatedList<User>> GetUsersPage(int pageIndex, int pageSize, string sortOrder, string searchTerm)
        {
            _logger.LogInformation("Entering GetUsersPage.");
            var users = ((IUsersRepository)_repo).GetPage(pageIndex, pageSize, sortOrder, searchTerm);
            var coreUsers = _factory.GetOfType<System.Collections.Generic.List<User>>();
            foreach (var user in users)
            {
                var coreUser = _converter.Convert(user);
                coreUser.Roles = (System.Collections.Generic.List<string>) await ((IUsersRepository)_repo).GetRolesAsync(user, new CancellationToken());
                coreUsers.Add(coreUser);                
            }
            var usersPage = _factory.GetUsersPage(coreUsers, _repo.GetCount(), pageSize, pageIndex);
            usersPage.ForEach(u => {
                _logger.LogDebug("User --->\n\t\tAddress: {0}\n" +
                    "\t\tEmail: {1}\n" +
                    "\t\tName: {2}\n" +
                    "\t\tPoints: {3}", u.Address, u.Email, u.Name, u.Points);
            });
            _logger.LogInformation("Leaving GetUsersPage.");
            return usersPage;
        }

        public async Task UpdateUserRoles(String userId)
        {
            var user = await _repo.GetById(userId);
            await UpdateUserRoles(user);
        }

        public async Task UpdateUserRoles(ApplicationUser user)
        {           
            if (Constants.MODERATOR_MINIMUM_RANK <= user.Points)
            {
                var userIsInRole = await _userManager.IsInRoleAsync(user, Constants.MODERATOR);
                if (!userIsInRole)
                {
                    await _userManager.AddToRoleAsync(user, Constants.MODERATOR);
                }
            }
            else if (Constants.EDITOR_MINIMUM_RANK <= user.Points)
            {
                var userIsInRole = await _userManager.IsInRoleAsync(user, Constants.MODERATOR);
                if (userIsInRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, Constants.MODERATOR);
                }
                userIsInRole = await _userManager.IsInRoleAsync(user, Constants.EDITOR);
                if (!userIsInRole)
                {
                    await _userManager.AddToRoleAsync(user, Constants.EDITOR);
                }
            }
            else if (Constants.VOTER_MINIMUM_RANK <= user.Points)
            {
                var userIsInRole = await _userManager.IsInRoleAsync(user, Constants.MODERATOR);
                if (userIsInRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, Constants.MODERATOR);
                }
                userIsInRole = await _userManager.IsInRoleAsync(user, Constants.EDITOR);
                if (userIsInRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, Constants.EDITOR);
                }
                userIsInRole = await _userManager.IsInRoleAsync(user, Constants.VOTER);
                if (!userIsInRole)
                {
                    await _userManager.AddToRoleAsync(user, Constants.VOTER);
                }
            }
            else
            {
                var userIsInRole = await _userManager.IsInRoleAsync(user, Constants.MODERATOR);
                if (userIsInRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, Constants.MODERATOR);
                }
                userIsInRole = await _userManager.IsInRoleAsync(user, Constants.EDITOR);
                if (userIsInRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, Constants.EDITOR);
                }
                userIsInRole = await _userManager.IsInRoleAsync(user, Constants.VOTER);
                if (userIsInRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, Constants.VOTER);
                }
            }
        }
    }
}
