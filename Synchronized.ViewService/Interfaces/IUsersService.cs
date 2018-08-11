using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel.UsersViewModels;

namespace Synchronized.ViewServices.Interfaces
{
    /// <summary>
    /// A Service for working with Users in the ViewModel Context.
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// A method for retreiving a PaginatedList of UserViewModel.
        /// </summary>
        /// <param name="pageIndex">The index of the Question.</param>
        /// <returns>An instance of PaginatedList of UserViewModel.</returns>
        Task<PaginatedList<UserViewModel>> GetIndexPage(int pageIndex);

        /// <summary>
        /// A method for retreiving a PaginatedList of UserViewModel.
        /// </summary>
        /// <param name="pageIndex">The index of the Question.</param>
        /// <param name="sortOrder">The order in which data should be sorted.</param>
        /// <param name="searchTerm">A term to search for in the data.</param>
        /// <returns>An instance of PaginatedList of UserViewModel.</returns>
        Task<PaginatedList<UserViewModel>> GetIndexPage(int pageIndex, string sortOrder, string searchTerm);

        /// <summary>
        /// A method for retreiving an instance of UserViewModel
        /// </summary>
        /// <param name="id">The Id of the required User.</param>
        /// <returns>An instance of a User, if one is available.</returns>
        Task<UserViewModel> GetDetailsPage(string id);
    }
}
