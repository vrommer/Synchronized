using Synchronized.Model;
using System.Collections.Generic;

namespace Synchronized.Repository.Interfaces
{
    public interface IUsersRepository
    {
        IEnumerable<ApplicationUser> GetAll();
    }
}
