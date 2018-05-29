using StructureMap;
using Synchronized.Core.Interfaces;
using Synchronized.Core.Utilities;
using Synchronized.Core.Utilities.Interfaces;

namespace Synchronized.Core.Infrastructure
{
    public class ServiceRegistry: Registry
    {
        public ServiceRegistry()
        {
            For<IDataConverter>().Use<CoreDataConverter>();
        }
    }
}
