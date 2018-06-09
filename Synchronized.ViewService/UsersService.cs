using Synchronized.SharedLib.Utilities;
using Synchronized.ViewServices.Interfaces;
using Synchronized.ViewModel.UsersViewModels;
using Synchronized.ViewModelFactories.Interfaces;
using Synchronized.UI.Utilities.Interfaces;

namespace Synchronized.ViewServices
{
    public class UsersService : IUsersService
    {
        private int pageSize = 20;
        private Core.Interfaces.IUsersService _usersService;
        private IUsersConverter _converter;
        private IViewModelFactory _factory;

        public UsersService(Core.Interfaces.IUsersService usersService, IUsersConverter dataConverter, IViewModelFactory factory)
        {
            _usersService = usersService;
            _converter = dataConverter;
            _factory = factory;
        }

        public PaginatedList<UserViewModel> GetIndexPage(int pageIndex)
        {        
            var users = _usersService.GetUsersPage(pageIndex, pageSize, null, null);
            var usersPage = _factory.GetPaginatedList<UserViewModel>(users.TotalSize, pageIndex, pageSize);

            users.ForEach(t => {
                var viewUser = _converter.Convert(t);
                usersPage.Add(viewUser);
            });
            return usersPage;
        }
    }
}
