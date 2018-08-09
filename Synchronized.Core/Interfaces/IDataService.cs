using Synchronized.SharedLib.Utilities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    /// <summary>
    /// This is a gemeric data service. It has method definitions that is relevant to all types of data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataService<T> where T: class
    {
        /// <summary>
        /// This async method is for retrieving an item of data using a predicate.
        /// </summary>
        /// <param name="predicate">A deligate function. A lambda can be used here.</param>
        /// <returns>Data item</returns>
        Task<T> GetBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// This async method is for retrieving an item of data using it's ID.
        /// </summary>
        /// <param name="Id">A string param</param>
        /// <returns>Data item</returns>
        Task<T> GetById(string Id);

        /// <summary>
        /// This method if for retrieving a Page of items.
        /// </summary>
        /// <param name="pageNumber">The number of required page</param>
        /// <param name="pageSize">The maximum number of items on a page</param>
        /// <param name="sortOrder">The order in which the data is sorted</param>
        /// <param name="filter">An expression to be search for in the Data Item context</param>
        /// <returns>Page of Data Items</returns>
        Task<PaginatedList<T>> GetPage(int pageNumber, int pageSize, string sortOrder, string filter);

        /// <summary>
        /// This method is for updating Data Item. It returns an indicator of whether the update was successful or not.
        /// </summary>
        /// <param name="entity">The Data Item to be updated</param>
        /// <returns>Boolean indicator of action success.</returns>
        Task<bool> Update(T entity);

    }
}
