using System.Threading.Tasks;

namespace Synchronized.SharedLib.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
