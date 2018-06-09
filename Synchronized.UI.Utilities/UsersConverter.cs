using Synchronized.UI.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using Synchronized.ServiceModel;
using Synchronized.ViewModel.UsersViewModels;
using Synchronized.ViewModelFactories.Interfaces;
using Synchronized.Core.Factories.Interfaces;

namespace Synchronized.UI.Utilities
{
    public class UsersConverter : IUsersConverter
    {

        private IViewModelFactory _viewModelFactory;
        private IServiceModelFactory _serviceModelFactory;

        public UsersConverter(IViewModelFactory viewModelFactory, IServiceModelFactory serviceModelFactory)
        {
            _viewModelFactory = viewModelFactory;
            _serviceModelFactory = serviceModelFactory;
        }

        public UserViewModel Convert(User from)
        {
            var user = _viewModelFactory.GetUser();
            user.Address = String.Copy(from.Address);
            user.Id = String.Copy(from.Id);
            user.ImageUri = String.Copy(from.ImageUri);
            user.Name = String.Copy(from.Name);
            user.Email = String.Copy(from.Email);
            return user;
        }

        public User Convert(UserViewModel from)
        {
            throw new NotImplementedException();
        }

        public List<UserViewModel> Convert(ICollection<User> from)
        {
            throw new NotImplementedException();
        }

        public List<User> Convert(ICollection<UserViewModel> from)
        {
            throw new NotImplementedException();
        }
    }
}
