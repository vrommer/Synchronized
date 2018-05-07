using Microsoft.EntityFrameworkCore;
using StructureMap;
using Synchronized.Core.Infrastructure;
using Synchronized.Core.Interfaces;
using Synchronized.Data;
using Synchronized.Repository.Interfaces;
using System.Linq;
using Xunit;

namespace Synchronized.WebApp.Test
{
    public class UnitTest1
    {   
        [Fact]
        public void include_a_registry()
        {
            var registry = new Registry();
            registry.IncludeRegistry<ServiceRegistry>();
            // build a container
            var container = new Container(registry);
            container.Configure(config =>
            {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(_ =>
                {
                    // Declare which assemblies to scan
                    //_.Assembly("Synchronized.WebApp.Test");
                    _.TheCallingAssembly();
                    //_.AssemblyContainingType<IQuestionsService>();
                    

                    // Built in registration conventions
                    _.AddAllTypesOf<IQuestionsService>().NameBy(x => x.Name.Replace("QuestionsService", ""));
                    _.WithDefaultConventions();
                });

                var option = new DbContextOptionsBuilder();
                var dbContextOptions = option.Options;

                config.AddRegistry(new ServiceRegistry());
                config.For<DbContext>().Use(t => new SynchronizedDbContext(dbContextOptions));
                //config.For<DbContext>().Use(new SynchronizedDbContext(option.Options));

                //Populate the container using the service collection
                //config.Populate(services);
            });
            // verify the default implementation and total registered implementations
            Assert.True(container.GetAllInstances<IQuestionsRepository>().Count() == 1);
        }
    }
}
