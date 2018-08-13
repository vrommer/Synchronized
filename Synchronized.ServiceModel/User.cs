using System.Threading.Tasks;
using Synchronized.ServiceModel.Interfaces;
using System.Collections.Generic;
using System;
using Synchronized.SharedLib.Interfaces;
using Synchronized.SharedLib.Services;

namespace Synchronized.ServiceModel
{
    /// <summary>
    /// This class represents a User in the Business Layer. The user is also a QuestionSubscriber.
    /// </summary>
    public class User: IQuestionSubscriber
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ImageUri { get; set; }
        public int Points { get; set; }
        public bool NewSubscriber { get; set; }
        public List<string> Roles { get; set; }
        public List<Question> Questions { get; set; }
        public DateTime JoiningDate { get; set; }
        public static readonly IEmailSender _emailSender = new EmailSender();

        public async Task Update()
        {
            await _emailSender.SendEmailAsync(Email, "subject", "message");
        }
    }
}
