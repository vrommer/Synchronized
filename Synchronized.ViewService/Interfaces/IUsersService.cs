using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel.UsersViewModels;

namespace Synchronized.ViewServices.Interfaces
{
    public interface IUsersService
    {
        PaginatedList<UserViewModel> GetIndexPage(int currentPage);
        Task<UserViewModel> GetDetailsPage(string id);
    }
}
