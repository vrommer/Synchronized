using Synchronized.Domain;

namespace Synchronized.Repository.Interfaces
{
    /// <summary>
    /// A repository for working with Posts in the Database.
    /// </summary>
    /// <typeparam name="TModel">TModel must be of Type Post.</typeparam>
    public interface IPostsRepository<TModel>: IDataRepository<TModel> where TModel: Post
    {
    }
}
