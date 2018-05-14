using Synchronized.ServiceModel;
using System.Collections.Generic;
using System.Linq;

namespace Synchronized.ViewModel
{
    public class DetailsViewModel
    {
        public Question Question{ get; set; }
        public Dictionary<string, Answer> Answers{ get; set; }
        public Dictionary<string, Comment> Comments{ get; set; }
        public Dictionary<string, Post> Posts { get; set; }

        public DetailsViewModel(Question question)
        {
            Question = question;
            Answers = new Dictionary<string, Answer>();
            Comments = new Dictionary<string, Comment>();
            Posts = new Dictionary<string, Post>
            {
                { question.Id, question }
            };
            question.Comments.ToList().ForEach(comment => {
                Comments.Add(comment.Id, comment);
                Posts.Add(comment.Id, comment);
            });
            question.Answers.ToList().ForEach(answer =>
            {
                Answers.Add(answer.Id, answer);
                Posts.Add(answer.Id, answer);
                answer.Comments.ToList().ForEach(comment => {
                    Comments.Add(comment.Id, comment);
                    Posts.Add(comment.Id, comment);
                });
            });
        }
    }
}
