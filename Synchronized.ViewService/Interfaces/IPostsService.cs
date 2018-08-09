using Synchronized.ViewModel;
using System.Threading.Tasks;

namespace Synchronized.ViewServices.Interfaces
{
    public interface IPostsService
    {
        Task<EditViewModel> GetPostForEdit(string postId);
        Task<string> EditPost(EditViewModel post);
    }
}
