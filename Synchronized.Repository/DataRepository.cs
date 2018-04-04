using Microsoft.EntityFrameworkCore;
using Synchronized.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Synchronized.Model;
using System.Threading.Tasks;

namespace Synchronized.Repository.Repositories
{
    public class DataRepository<TModel> : IDataRepository<TModel> where TModel : class, IEntity
    {
        protected DbContext _context;
        protected DbSet<TModel> _dbSet;

        bool disposed = false;

        public DataRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TModel>();
        }

        public void Add(TModel item)
        {
            _dbSet.Add(item);
        }

        public int CountItems()
        {
            throw new NotImplementedException();
        }

        public void Delete(string itemId)
        {
            var item =_dbSet.Find(itemId);
            _dbSet.Remove(item);
        }

        public IEnumerable<TModel> FindBy(Expression<Func<TModel, bool>> predicate)
        {
            IEnumerable<TModel> results = _dbSet.AsNoTracking()
                .Where(predicate).ToList();
            return results;
        }

        public TModel FindById(string itemId)
        {
            return _dbSet.AsNoTracking().SingleOrDefault(e => e.Id.Equals(itemId));
        }

        public IEnumerable<TModel> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public async Task<int> GetCount()
        {
            int count = await _dbSet.CountAsync();
            return count;
        }

        public void Update(TModel item)
        {
            _dbSet.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task<List<TModel>> GetPage(int pageIndex, int pageSize)
        {
            var source = _dbSet.AsNoTracking();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return items;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DataRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
