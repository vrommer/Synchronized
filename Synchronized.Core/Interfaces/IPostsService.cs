using SharedLib.Infrastructure.Constants;
using Synchronized.ServiceModel;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synchronized.Core.Interfaces
{
    public interface IPostsService<TEntity>: IDataService<TEntity> where TEntity: Post
    {
    }
}
