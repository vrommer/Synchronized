using Synchronized.Domain;
using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;
using System;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    /// <summary>
    /// This interface defines methods for working with users in the context of business logic.
    /// </summary>
    public interface IUsersService : IDataService<User>
    {
        /// <summary>
        /// This method is for retreiving a page of Users.
        /// </summary>
        /// <param name="pageIndex">The index of the page.</param>
        /// <param name="pageSize">Number of users per page.</param>
        /// <param name="sortOrder">Order in which data should be sorted</param>
        /// <param name="searchTerm">A term that has to appear somewhere in the data.</param>
        /// <returns></returns>
        Task<PaginatedList<User>> GetUsersPage(int pageIndex, int pageSize, string sortOrder, string searchTerm);

        /// <summary>
        /// This method is for Updating Roles of Users.
        /// </summary>
        /// <param name="userId">The Id of the user for Update.</param>
        Task UpdateUserRoles(String userId);

        /// <summary>
        /// This method is for Updating Roles of Users.
        /// </summary>
        /// <param name="user">The user we wish to update roles for.</param>
        Task UpdateUserRoles(ApplicationUser user);


        /// <summery>
        /// Count the total number of users
        int CountUser();
    }
}
