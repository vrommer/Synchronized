using Synchronized.Domain;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Repository.Interfaces
{
    public interface IPostsRepository<TModel>: IDataRepository<TModel> where TModel: Post
    {
    }
}
