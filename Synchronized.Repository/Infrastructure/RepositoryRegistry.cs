using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StructureMap;
using Synchronized.Data;
using Synchronized.Domain;
using Synchronized.Repository.Interfaces;
using Synchronized.Repository.Repositories;

namespace Synchronized.Repository.Infrastructure
{
    public class RepositoryRegistry: Registry
    {
        public RepositoryRegistry()
        {
        }
    }
}
