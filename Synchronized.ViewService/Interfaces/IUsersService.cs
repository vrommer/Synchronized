using Synchronized.SharedLib.Utilities;
using Synchronized.ViewModel.UsersViewModels;

namespace Synchronized.ViewServices.Interfaces
{
    public interface IUsersService
    {
        PaginatedList<UserViewModel> GetIndexPage(int currentPage);
    }
}
