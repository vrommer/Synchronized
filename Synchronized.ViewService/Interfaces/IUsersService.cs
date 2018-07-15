using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel.UsersViewModels;

namespace Synchronized.ViewServices.Interfaces
{
    public interface IUsersService
    {
        Task<PaginatedList<UserViewModel>> GetIndexPage(int pageIndex);
        Task<PaginatedList<UserViewModel>> GetIndexPage(int pageIndex, string sortOrder, string searchTerm);
        Task<UserViewModel> GetDetailsPage(string id);
    }
}
