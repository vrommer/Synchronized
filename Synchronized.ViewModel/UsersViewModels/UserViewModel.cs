using Synchronized.SharedLib;
using Synchronized.ViewModel.QuestionsViewModels;
using System.Collections.Generic;

namespace Synchronized.ViewModel.UsersViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public string Address { get; set; }
        public int Points { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public string MajorRole
        {
            get
            {
                string majorRole;
                var roleNames = Roles.Split(',');
                var rolesList = new List<string>(roleNames);
                if (rolesList.Contains(Constants.MODERATOR))
                {
                    majorRole = Constants.MODERATOR;
                }
                else if (rolesList.Contains(Constants.EDITOR))
                {
                    majorRole = Constants.EDITOR;
                }
                else if (rolesList.Contains(Constants.VOTER))
                {
                    majorRole = Constants.VOTER;
                }
                else
                {
                    majorRole = "";
                }
                return majorRole;
            }
        }
        public List<QuestionForHomePage> ActivePosts { get; set; }
    }
}
