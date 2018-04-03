using Synchronized.Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.Repository.Interfaces
{
    public interface IPostRepository<TPost> : IDataRepository<TPost>  where TPost: Post
    {
    }
}
