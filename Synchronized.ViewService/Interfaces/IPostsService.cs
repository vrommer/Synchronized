using Synchronized.ViewModel;
using System.Threading.Tasks;

namespace Synchronized.ViewServices.Interfaces
{
    /// <summary>
    /// This interface defines methods for working with Posts in the ViewModel Context.
    /// </summary>
    public interface IPostsService
    {
        /// <summary>
        /// A method for retreiving a PostForEdit instance.
        /// </summary>
        /// <param name="postId">The Id of the Post.</param>
        /// <returns>An instance of EditViewModel.</returns>
        Task<EditViewModel> GetPostForEdit(string postId);

        /// <summary>
        /// A method for editing Posts int the ViewModel Context.
        /// </summary>
        /// <param name="post">An instance of an Edited Post.</param>
        /// <returns>PostId if the Edit is successful.</returns>
        Task<string> EditPost(EditViewModel post);
    }
}
