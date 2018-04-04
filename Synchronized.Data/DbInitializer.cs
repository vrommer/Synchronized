using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Synchronized.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synchronized.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SynchronizedDbContext context = new SynchronizedDbContext();
            context.Database.EnsureCreated();
            Random rand = new Random();
            int maxPoints = 1000;
            int maxViews = 100;
            int numOfQuestions = 100;
            int numOfAnswers = 100;
            int numOfComments = 100;
            int minimumTagsInQuestion = 1;
            int maximumTagsInQuestion = 4;

            string[] userNames =
            {
                "Viktoriya \u2764", "Vadim", "Yossi", "Nati", "Isaac", "Oren", "Allen \u2764", "Nir", "Netta", "Katty", "Soffi", "Ira", "Marina", "Igal", "Juli", "Aprana", "Prasana", "Tigran", "Anton", "Sergey", "Dima", "Michael",
                "Alex", "Kostya", "Max", "Lee", "Keren", "Dina", "Maayan", "Idan", "Adam", "Ari", "Arik", "Yariv", "Naor", "Oron", "Yevgeni", "Bruce", "Kim", "Joseph", "Ido", "Jack", "John", "Romeo",
                "Roman", "Rita", "Irena", "Vladislav", "Rostislav", "Josh", "Kevin", "Devin", "Miika", "Luca", "Fabio", "Fredd", "Leon", "Arthur", "Boris", "David", "Eithan", "Stephany", "Christine", "Alon", "Alona", "Olga",
                "Sharon", "Hellen", "Hulio", "Enrique", "Darwin", "Stephan", "Joe", "Hillary", "Barak", "Benjamin", "Ashton", "Cameron", "Kventin", "Guy", "Ahmed", "Muhammad", "Gadir", "Kamila", "Polina", "Pola", "Marga", "Sandra"
            };

            List<string> tagNames = new List<string>
            {
                "coffee-script",
                "less",
                "java",
                "javascript",
                "python",
                "c#",
                ".net",
                "maven",
                "gulp",
                "grunt",
                "c++",
                "c",
                "visual-basic",
                "ant",
                "react",
                "shell-script",
                "angular.js",
                "docker",
                "cheff",
                "objective-c",
                "php",
                "jenkins",
                "node.js",
                "express.js",
                "coa",
                "terraform",
                "assembly",
                "html5",
                "css3",
                "jquery",
                "jquery-ui",
                "bootstrap",
                "font-awesome",
                "jenkins-pipline",
                "groovy",
                "grales",
                "grape",
                "ruby",
                "entity-framework-core",
                "entity-framework",
                "asp.net-core"
            };

            // number of tags
            int numOfTags = tagNames.Count();

            // IDs of ApplicationUsers
            List<string> UserIds = new List<string>();

            // IDs of Questions
            List<string> questionIds = new List<string>();

            // IDs of Answers
            List<string> answerIds = new List<string>();

            // IDs of tags
            List<string> tagIds = new List<string>();

            if (context.Posts.Any())
            {
                return;   // DB has been seeded
            }

            string roleName = "Moderator";
            string password = "Abcd@1234";

            bool roleExists = await roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var joseph = new ApplicationUser { Email = "mail1@example.com", EmailConfirmed = true, UserName = "joseph" };

            var result = await userManager.CreateAsync(joseph, password);

            bool userIsInRole = await userManager.IsInRoleAsync(joseph, roleName);

            if (result.Succeeded && !userIsInRole)
            {
                result = await userManager.AddToRoleAsync(joseph, roleName);
            }

            /***********************************************************************
             * Users
             ***********************************************************************/

            var users = new List<ApplicationUser>();
            for (int i = 0; i < userNames.Length; i++)
            {
                users.Add(new ApplicationUser {
                    Email = "mail" + i + "@example.com", UserName = userNames[i], Points = rand.Next(maxPoints)
                });
            }

            users.ForEach(async item =>
            {
                result = await userManager.CreateAsync(item, password);
                if (result.Succeeded)
                {
                    UserIds.Add(item.Id);
                }
            });

            /***********************************************************************
             * Questions
             ***********************************************************************/

            var questions = new List<Question>();

            for (int i = 0; i < numOfQuestions; i++)
            {
                questions.Add(new Question
                {
                    Title = "The title of the Question",
                    Content = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                    "Fusce massa libero, hendrerit non risus a, sollicitudin venenatis tellus. Cras in enim lectus. " +
                    "Nunc nisi metus, condimentum vel urna eget, rutrum feugiat est. Curabitur eget dui eu sapien " +
                    "dictum vulputate vitae eu diam. Suspendisse iaculis, lorem in semper pharetra, felis dui ultrices " +
                    "massa, volutpat rutrum sem nisi nec velit. Donec interdum convallis massa at interdum. Mauris luctus " +
                    "tellus arcu, vitae porttitor leo finibus sed. Suspendisse ante augue, mattis sit amet laoreet in, sodales " +
                    "id libero. Cras in dictum dui, ut tempor lectus. Pellentesque enim tellus, accumsan non malesuada aliquet, " +
                    "posuere ac lorem. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos." +
                    "</p>\n<p>Proin sed dictum erat. Proin interdum lacus in nisl consequat maximus. Morbi aliquet vehicula " +
                    "magna ut pellentesque. In auctor justo pulvinar commodo volutpat. Suspendisse risus lectus, accumsan " +
                    "et pellentesque nec, efficitur vel urna. Etiam ante turpis, fermentum quis lorem ut, dapibus iaculis " +
                    "elit. Proin ut ex sed erat lobortis euismod in at turpis. Aliquam aliquet ante non ipsum lacinia ornare. " +
                    "Donec in eros non urna semper bibendum. Nunc vehicula cursus ex, nec viverra odio fermentum ut. Praesent rhoncus " +
                    "efficitur scelerisque.</p>\n<p>Curabitur quis aliquam eros. Nam sed risus sed tellus dignissim euismod non in " +
                    "enim. Mauris ac semper elit. Proin ornare aliquam ligula, et sagittis quam ultricies vitae. Duis fringilla " +
                    "euismod tincidunt. Etiam consequat leo a nulla eleifend, et dignissim lacus finibus. Vestibulum nulla massa, " +
                    "dignissim ac eleifend eleifend, scelerisque auctor neque. Maecenas feugiat, ligula at ultrices laoreet, sem massa " +
                    "efficitur risus, at ullamcorper nisi quam a dui. Etiam et lectus quis mauris scelerisque ultricies id a orci." +
                    " Suspendisse porta felis et diam semper aliquam.</p>\n    <p>Aliquam sit amet est in mauris efficitur " +
                    "convallis. Praesent vitae volutpat odio. Maecenas hendrerit, mi commodo imperdiet ornare, justo neque " +
                    "elementum sem, eu pharetra nibh neque non dui. Curabitur blandit semper enim at tincidunt. In tellus " +
                    "nisi, eleifend ac sem vel, iaculis consequat ipsum. Sed tellus urna, ultricies vel justo ac, volutpat " +
                    "sollicitudin mi. Sed sollicitudin lacus in lorem mattis fringilla. Quisque varius rutrum justo " +
                    "elementum venenatis. Curabitur ac nibh ac nibh dictum maximus. Mauris lacinia nulla cursus " +
                    "neque pulvinar, semper fermentum ligula sollicitudin. Nunc tellus nibh, scelerisque sed posuere " +
                    "ut, feugiat in neque.</p>\n<p>Cras luctus neque non lacus blandit, ut laoreet justo condimentum. " +
                    "Vestibulum venenatis luctus mauris quis condimentum. Maecenas suscipit quam ac dolor mattis, " +
                    "ut dictum mi tincidunt. Etiam posuere quam augue, a tempor odio congue ut. Praesent est enim" +
                    ", sollicitudin sed ornare id, euismod at eros. Proin tincidunt sed ex sit amet pulvinar. Sed " +
                    "blandit leo vel ex mollis malesuada. Sed volutpat, leo non sagittis malesuada, lectus enim " +
                    "bibendum turpis, id eleifend nisi velit ut libero. Quisque non metus nec nulla tincidunt " +
                    "dignissim. Fusce orci ipsum, faucibus et blandit eu, consequat id ipsum. Maecenas bibendum, " +
                    "est nec dapibus euismod, turpis magna tristique leo, in lobortis ligula metus nec metus. " +
                    "Nam libero turpis, ultricies quis leo at, dictum fermentum diam.</p>",
                    Points = rand.Next(maxPoints),
                    PublisherId = UserIds[rand.Next(UserIds.Count)],
                    Views = rand.Next(maxViews)
                });
            }

            questions.ForEach(q => context.Posts.Add(q));
            context.SaveChanges();

            context.Posts.OfType<Question>().ToList().ForEach(q => questionIds.Add(q.Id));

            /***********************************************************************
             * Answers
             ***********************************************************************/

            var answers = new List<Answer>();
            for (int i = 0; i < numOfAnswers; i++)
            {
                int pointer = rand.Next(numOfQuestions);
                Question target = context.Posts.OfType<Question>().Where(q => q.Id == questionIds[pointer]).Include(q => q.Answers).ToArray()[0];
                Answer a = new Answer
                {
                    Content = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                    "Fusce massa libero, hendrerit non risus a, sollicitudin venenatis tellus. Cras in enim lectus. " +
                    "Nunc nisi metus, condimentum vel urna eget, rutrum feugiat est. Curabitur eget dui eu sapien " +
                    "dictum vulputate vitae eu diam. Suspendisse iaculis, lorem in semper pharetra, felis dui ultrices " +
                    "massa, volutpat rutrum sem nisi nec velit. Donec interdum convallis massa at interdum. Mauris luctus " +
                    "tellus arcu, vitae porttitor leo finibus sed. Suspendisse ante augue, mattis sit amet laoreet in, sodales " +
                    "id libero. Cras in dictum dui, ut tempor lectus. Pellentesque enim tellus, accumsan non malesuada aliquet, " +
                    "posuere ac lorem. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos." +
                    "</p>\n<p>Proin sed dictum erat. Proin interdum lacus in nisl consequat maximus. Morbi aliquet vehicula " +
                    "magna ut pellentesque. In auctor justo pulvinar commodo volutpat. Suspendisse risus lectus, accumsan " +
                    "et pellentesque nec, efficitur vel urna. Etiam ante turpis, fermentum quis lorem ut, dapibus iaculis " +
                    "elit. Proin ut ex sed erat lobortis euismod in at turpis. Aliquam aliquet ante non ipsum lacinia ornare. " +
                    "Donec in eros non urna semper bibendum. Nunc vehicula cursus ex, nec viverra odio fermentum ut. Praesent rhoncus " +
                    "efficitur scelerisque.</p>\n<p>Curabitur quis aliquam eros. Nam sed risus sed tellus dignissim euismod non in " +
                    "enim. Mauris ac semper elit. Proin ornare aliquam ligula, et sagittis quam ultricies vitae. Duis fringilla ",
                    IsAccepted = !target.Answered() && rand.Next(2) > 0 ? true : false,
                    Points = rand.Next(maxPoints),
                    PublisherId = UserIds[rand.Next(UserIds.Count)],
                    QuestionId = target.Id,
                };
                context.Add(a);
                context.SaveChanges();
            }

            context.Posts.OfType<Answer>().ToList().ForEach(a => answerIds.Add(a.Id));

            /***********************************************************************
             * Comments
             ***********************************************************************/
            var comments = new List<Comment>();
            for (int i = 0; i < numOfComments; i++)
            {
                comments.Add(new Comment
                {
                    Content = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                    "Fusce massa libero, hendrerit non risus a, sollicitudin venenatis tellus. Cras in enim lectus. " +
                    "Nunc nisi metus, condimentum vel urna eget, rutrum feugiat est. Curabitur eget dui eu sapien " +
                    "dictum vulputate vitae eu diam. Suspendisse iaculis, lorem in semper pharetra, felis dui ultrices " +
                    "massa, volutpat rutrum sem nisi nec velit. Donec interdum convallis massa at interdum. Mauris luctus " +
                    "tellus arcu, vitae porttitor leo finibus sed.",
                    PostId = rand.Next(2) > 0 ? questionIds[rand.Next(numOfQuestions)]: answerIds[rand.Next(numOfAnswers)]
                });
            }
            comments.ForEach(c => context.Posts.Add(c));
            context.SaveChanges();
            /***********************************************************************
            * Tags
            ***********************************************************************/
            var tags = new List<Tag>();
            for (int i = 0; i < tagNames.Count; i++)
            {
                var tag = new Tag
                {
                    Name = tagNames[i],
                    Description = "Repurposing user stories with the possibility to be CMSable.",
                    PublisherId = UserIds[rand.Next(UserIds.Count)]
                };
                //context.SaveChanges();
                context.Tags.Add(tag);
            }
            context.SaveChanges();
            context.Tags.ToList().ForEach(t => tagIds.Add(t.Id));

            /***********************************************************************
            * QuestionTags
            ***********************************************************************/
            context.Posts.OfType<Question>().ToList().ForEach(q => {
                var numOfTagsForQuestion = rand.Next(minimumTagsInQuestion, maximumTagsInQuestion);
                var questionTag = new QuestionTag {
                    QuestionId = q.Id,
                    TagId = tagIds[rand.Next(tagIds.Count)]
                };
                context.QusetionTags.Add(questionTag);
            });
            context.SaveChanges();
        }
    }
}
