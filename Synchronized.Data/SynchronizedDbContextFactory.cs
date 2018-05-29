using Microsoft.EntityFrameworkCore.Design;

namespace Synchronized.Data
{
    public class SynchronizedDbContextFactory : IDesignTimeDbContextFactory<SynchronizedDbContext>
    {
        public SynchronizedDbContext CreateDbContext(string[] args)
        {
            return new SynchronizedDbContext(@"Server = (localdb)\mssqllocaldb; Database = SynchronizedData; Trusted_Connection = true");
        }
    }
}
