using Synchronized.SharedLib.Utilities;
using Synchronized.ViewServices.Interfaces;
using Synchronized.ViewModel.UsersViewModels;
using Synchronized.ViewModelFactories.Interfaces;
using Synchronized.UI.Utilities.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Synchronized.ViewServices
{
    public class UsersService : IUsersService
    {
        private int pageSize = 20;
        private Core.Interfaces.IUsersService _usersService;
        private IUsersConverter _converter;
        private IViewModelFactory _factory;
        private ILogger<UsersService> _logger;

        public UsersService(ILogger<UsersService> logger, Core.Interfaces.IUsersService usersService, IUsersConverter dataConverter, IViewModelFactory factory)
        {
            _usersService = usersService;
            _converter = dataConverter;
            _factory = factory;
            _logger = logger;
        }

        public async Task<UserViewModel> GetDetailsPage(string id)
        {
            var user = await _usersService.GetById(id);
            var viewUser = _converter.Convert(user);
            return viewUser;
        }

        public PaginatedList<UserViewModel> GetIndexPage(int pageIndex)
        {
            _logger.LogInformation("Entering GetIndexPage.");
            var users = _usersService.GetUsersPage(pageIndex, pageSize, null, null);
            var usersPage = _factory.GetPaginatedList<UserViewModel>(users.TotalSize, pageIndex, pageSize);

            users.ForEach(t =>
            {
                var viewUser = _converter.Convert(t);
                _logger.LogDebug("User --->\n\t\tAddress: {0}\n" +
                    "\t\tEmail: {1}\n" +
                    "\t\tName: {2}\n" +
                    "\t\tPoints: {3}", t.Address, t.Email, t.Name, t.Points);
                usersPage.Add(viewUser);
            });
            _logger.LogInformation("Leaving GetDetailsPage.");
            return usersPage;
        }
    }
}
