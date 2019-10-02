using Synchronized.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    /// <summary>
    /// A generic repository for IEntity. Defines methods that are relevant to any type of Data.
    /// </summary>
    /// <typeparam name="T">The type for which we want to implement this repository.</typeparam>
    public interface IDataRepository<T> where T: class, IEntity
    {
        /// <summary>
        /// Get an Item from the database by entityId.
        /// </summary>
        /// <param name="entityId">The Id of the retreived Entity.</param>
        /// <returns>An instance of T</returns>
        Task<T> GetById(string entityId);

        /// <summary>
        /// Get an Item from the database by an Expression.
        /// </summary>
        /// <param name="predicate">A delegate function that returns a Boolean. A Lambda may be used.</param>
        /// <returns></returns>
        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retreives a Page of T from the Database.
        /// </summary>
        /// <param name="pageNumber">The number of the required page.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="searchTerm">A search term.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>A List of T.</returns>
        List<T> GetPage(int pageNumber, int pageSize, string searchTerm, string sortOrder);

        /// <summary>
        /// This method is for adding items to the Database.
        /// </summary>
        /// <param name="entity">A new instance of T</param>
        /// <returns>The Id of the created entity, or Empty Id if entity is not created.</returns>
        Task<string> AddAsync(T entity);

        /// <summary>
        /// Update an instance of TEntity in the Database.
        /// </summary>
        /// <typeparam name="TEntity">TEntity must be of type class.</typeparam>
        /// <param name="Entity">The Entity we wish to Update.</param>
        Task UpdateAsync<TEntity>(TEntity Entity) where TEntity: class;

        /// <summary>
        /// Delete an Entity from the Database.
        /// </summary>
        /// <param name="entity">The entity we wish to Delete.</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Get the total database number of Entities in the Database
        /// </summary>
        /// <returns></returns>
        int GetCount();

        Task<bool> DeleteCommentAsync(string commentId);
    }
}
