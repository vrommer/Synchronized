using StructureMap;
using Synchronized.Core.Interfaces;

namespace Synchronized.Core.Infrastructure
{
    public class ServiceRegistry: Registry
    {
        public ServiceRegistry()
        {
            For(typeof(IDataService<>)).Use(typeof(DataService<>));
        }
    }
}
