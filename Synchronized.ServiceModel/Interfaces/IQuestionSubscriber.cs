using System.Threading.Tasks;

namespace Synchronized.ServiceModel.Interfaces
{
    public interface IQuestionSubscriber
    {
        string Email { get; set; }
        string Id { get; set; }
        bool NewSubscriber { get; set; }
        Task Update();
    }
}
