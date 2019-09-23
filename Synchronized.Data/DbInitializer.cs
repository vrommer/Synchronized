using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Synchronized.Domain;
using Synchronized.SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronized.Data
{
    public class DbInitializer
    {
        // number of tags
        static int numOfTags;

        // IDs of ApplicationUsers
        static List<string> UserIds = new List<string>();

        // IDs of Questions
        static List<string> questionIds = new List<string>();

        // IDs of Answers
        static List<string> answerIds = new List<string>();

        // IDs of tags
        static List<string> tagIds = new List<string>();

        static SynchedIdentityDbContext context;
        static Random rand = new Random();
        static int totalViews = 500;
        static int totalVotes = 500;
        static int numOfQuestions = 100;
        static int pageSize = 20;
        static int numOfPages = numOfQuestions/pageSize;
        static int numOfAnswers = 200;
        static int numOfComments = 400;

        //static int totalViews = 0;
        //static int totalVotes = 0;
        //static int numOfQuestions = 3;
        //static int numOfAnswers = 3;
        //static int numOfComments = 5;

        static int minimumTagsInQuestion = 1;
        static int maximumTagsInQuestion = 4;

        public static async Task Initialize(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, DbContext Context)
        {
            context = (SynchedIdentityDbContext)Context;
            context.Database.EnsureCreated();

            string[] userNames =
            {
                "joseph", "Viktoriya", "Vadim", "Yossi", "Nati", "Isaac", "Oren", "Allen", "Nir", "Netta", "Katty", "Soffi", "Ira", "Marina", "Igal", "Juli", "Aprana", "Prasana", "Tigran", "Anton", "Sergey", "Dima", "Michael",
                "Alex", "Kostya", "Max", "Lee", "Keren", "Dina", "Maayan", "Idan", "Adam", "Ari", "Arik", "Yariv", "Naor", "Oron", "Yevgeni", "Bruce", "Kim", "Joseph", "Ido", "Jack", "John", "Romeo",
                "Roman", "Rita", "Irena", "Vladislav", "Rostislav", "Josh", "Kevin", "Devin", "Miika", "Luca", "Fabio", "Fredd", "Leon", "Arthur", "Boris", "David", "Eithan", "Stephany", "Christine", "Alon", "Alona", "Olga",
                "Sharon", "Hellen", "Hulio", "Enrique", "Darwin", "Stephan", "Joe", "Hillary", "Barak", "Benjamin", "Ashton", "Cameron", "Kventin", "Guy", "Ahmed", "Muhammad", "Gadir", "Kamila", "Polina", "Pola", "Marga", "Sandra", "Alexa"
            };

            //string[] userNames =
            //{
            //    "Viktoriya", "Vadim", "Yossi", "Nati", "Isaac", "Oren", "Allen", "Nir", "Netta", "Katty", "Soffi", "Ira", "Marina", "Igal"
            //};

            //            string[] userNames =
            //{
            //                "Viktoriya", "Vadim", "Yossi", "Nati"
            //            };

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

            List<string> roles = new List<string>
            {
                Constants.VOTER,
                Constants.EDITOR,
                Constants.MODERATOR

            };

            // number of tags
            numOfTags = tagNames.Count();

            if (context.Posts.Any())
            {
                return;   // DB has been seeded
            }

            foreach (string role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            /***********************************************************************
             * Users
             ***********************************************************************/

            var users = new List<ApplicationUser>();
            string password = "Abcd@1234";
            for (int i = 1; i < userNames.Length; i++)
            {
                var user = new ApplicationUser
                {
                    Email = userNames[i] + "@example.com",
                    UserName = userNames[i],
                    ImageUri = "/pictures/user_default.png"
                };
                if (i <= 4)
                {
                    user.Points = 4000;
                }
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    users.Add(user);
                    UserIds.Add(user.Id);
                }
            }

            /***********************************************************************
             * Questions
             ***********************************************************************/

            var questions = new List<Question>();

            for (int i = 0; i < numOfQuestions; i++)
            {
                var publisherId = UserIds[rand.Next(UserIds.Count)];
                var question = new Question
                {
                    Body = "<span style=\"color: rgb(114, 114, 114); font - family: " +
                    "-apple - system, BlinkMacSystemFont, &quot; Segoe UI&quot;, Roboto, " +
                    "&quot; Helvetica Neue&quot;, Arial, sans - serif, &quot; Apple Color " +
                    "Emoji & quot;, &quot; Segoe UI Emoji & quot;, &quot; Segoe UI Symbol " +
                    "& quot; ; font - size: 16px; \">This is the body of the question. The body" +
                    " of the question is created using reach text editor, and it is formatted " +
                    "to HTML in background. Available styles are Normal and Code. This is used " +
                    "to separate code from other text. In the future, it may be available to " +
                    "create a visual representation for code.</span><p style=\"margin - bottom: " +
                    "1rem; color: rgb(114, 114, 114); font - family: -apple - system, " +
                    "BlinkMacSystemFont, &quot; Segoe UI&quot;, Roboto, &quot; Helvetica Neue&quot;" +
                    ", Arial, sans - serif, &quot; Apple Color Emoji & quot;, &quot; Segoe UI Emoji" +
                    " & quot;, &quot; Segoe UI Symbol & quot; ; font - size: 16px; \"><br></p><p" +
                    " style=\"margin - bottom: 1rem; color: rgb(114, 114, 114); font - family:" +
                    " -apple - system, BlinkMacSystemFont, &quot; Segoe UI&quot;, Roboto, &quot;" +
                    " Helvetica Neue&quot;, Arial, sans - serif, &quot; Apple Color Emoji & quot;," +
                    " &quot; Segoe UI Emoji & quot;, &quot; Segoe UI Symbol & quot; ; font - size:" +
                    " 16px; \">Below is the section of tags. When asking a question, an auto-complete" +
                    " is available for tags. If tag is not in autocomplete it may not be inserted" +
                    " into the field. This is client side validation, which of course could be" +
                    " overridden. To add a tag user may select a tag and than press enter for the" +
                    " tag to be added.</p>",
                    //Points = rand.Next(maxPoints),
                    PublisherId = publisherId,
                    Subscriptions = new List<Subscription>()
                    {
                        new Subscription()
                        {
                            UserId = publisherId
                        }
                    }
                };
                if (i == 0)
                {
                    question.Title = "This is the 1st Question.";
                }
                else if (i == 1)
                {
                    question.Title = "This is the 2nd Question.";
                }
                else if (i == 2)
                {
                    question.Title = "This is the 3rd Question.";
                }
                else
                {
                    question.Title = String.Format("This is the {0}th Question", i);
                }
                questions.Add(question);
            }

            questions.ForEach(q => context.Set<Question>().Add(q));
            await context.SaveChangesAsync();

            context.Posts.OfType<Question>().ToList().ForEach(q => questionIds.Add(q.Id));

            /***********************************************************************
             * Answers
             ***********************************************************************/

            var answers = new List<Answer>();
            for (int i = 0; i < numOfAnswers; i++)
            {
                var publisherId = UserIds[rand.Next(UserIds.Count)];
                int pointer = rand.Next(numOfQuestions);
                Question target = context.Posts.OfType<Question>().Where(q => q.Id == questionIds[pointer]).Include(q => q.Publisher).Include(q => q.Answers).ToArray()[0];
                Answer a = new Answer
                {
                    Body = "This is the body of the answer to the question. Like the body of the answer, it is also created using a reach text editor. " +
                    "The editor options are standard reach text editor's options such as those found in popular text editor's like word, and therefore are not documented. " +
                    "Their documentation and use are widely spread This text editor is jQuery based text editor named jquery TE. Unfortunately, this text editor does not " +
                    "implement basic keyboard navigation options such as support for CTRL+ARROW or CTRL+SHIFT+ARROW.",
                    IsAccepted = !target.Answered() && rand.Next(2) > 0 ? true : false,
                    //Points = rand.Next(maxPoints),
                    PublisherId = publisherId,
                    QuestionId = target.Id
                };
                var sub = new Subscription() {
                    UserId = publisherId,
                    QuestionId = target.Id
                };
                if (!target.Subscriptions.Contains(sub))
                {
                    target.Subscriptions.Add(new Subscription()
                    {
                        UserId = publisherId
                    });
                }
                context.Update(target);
                context.Add(a);
                if (a.IsAccepted)
                {
                    a.Publisher.Points += Constants.ANSWER_ACCEPT_ANSWERER_BONUS;
                    var question = context.Set<Question>().Include(q => q.Publisher).Where(q => q.Id.Equals(a.QuestionId)).SingleOrDefault();
                    question.Publisher.Points += Constants.ANSWER_ACCEPT_ACCEPTER_BONUS;
                    context.Set<Question>().Update(question);
                }
                context.SaveChanges();
            }
            //context.SaveChanges();

            context.Posts.OfType<Answer>().ToList().ForEach(a => answerIds.Add(a.Id));

            /***********************************************************************
             * Comments
             ***********************************************************************/
            var comments = new List<Comment>();
            for (int i = 0; i < numOfComments; i++)
            {
                var publisherId = UserIds[rand.Next(UserIds.Count)];
                var randomNumber = rand.Next(2);
                var postId = randomNumber > 0 ? questionIds[rand.Next(numOfQuestions)] : answerIds[rand.Next(numOfAnswers)];
                if (randomNumber > 0)
                {
                    var question = context.Set<Question>().AsNoTracking().Where(q => q.Id.Equals(postId)).Include(q => q.Subscriptions).SingleOrDefault();
                    if (!question.Subscriptions.Contains(new Subscription()
                    {
                        UserId = publisherId,
                        QuestionId = question.Id
                    }))
                    {
                        question.Subscriptions.Add(new Subscription()
                        {
                            UserId = publisherId
                        });
                    }
                }
                else
                {
                    var answer = context.Set<Answer>().Where(a => a.Id.Equals(postId)).SingleOrDefault();
                    var question = context.Set<Question>().AsNoTracking().Where(q => q.Id.Equals(answer.QuestionId)).Include(q => q.Subscriptions).SingleOrDefault();
                    if (!question.Subscriptions.Contains(new Subscription()
                    {
                        UserId = publisherId,
                        QuestionId = question.Id
                    }))
                    {
                        question.Subscriptions.Add(new Subscription()
                        {
                            UserId = publisherId
                        });
                    }
                }
                comments.Add(new Comment
                {
                    Body = "Questions and Answers can also have comments. Comments may be deleted or edited by users who have the required permissions. Questions and answers may have many comments. ",
                    PostId = postId, 
                    PublisherId = publisherId
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
                    Id = tagNames[i],
                    Description = "Repurposing user stories with the possibility to be CMSable.",
                    PublisherId = UserIds[rand.Next(UserIds.Count)]
                };
                //context.SaveChanges();
                context.Tags.Add(tag);
                context.SaveChanges();
            }
            context.Tags.ToList().ForEach(t => tagIds.Add(t.Id));

            //Thread.Sleep(30000);

            /***********************************************************************
            * QuestionTags
            ***********************************************************************/
            HashSet<int> integers;
            for (int i = 0; i < numOfPages; i++)
            {
                var questionsList = await context.Posts.OfType<Question>()
                    .Skip(i*pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                foreach (Question q in questionsList)
                {
                    int numOfTagsForQuestion = rand.Next(minimumTagsInQuestion, maximumTagsInQuestion);
                    integers = new HashSet<int>();
                    for (int j = 1; j <= numOfTagsForQuestion; j++)
                    {
                        var tagsListPointer = rand.Next(tagIds.Count);
                        while (integers.Contains(tagsListPointer))
                        {
                            tagsListPointer = rand.Next(tagIds.Count);
                        }
                        integers.Add(tagsListPointer);
                        var questionTag = new QuestionTag
                        {
                            QuestionId = q.Id,
                            TagId = tagIds[tagsListPointer]
                        };
                        context.QusetionTags.Add(questionTag);
                    }
                }
            }
            context.SaveChanges();
          
            /***********************************************************************
            * QuestionViews
            ***********************************************************************/
            var questionViews = new HashSet<QuestionView>();

            for (int i = 0; i < totalViews; i++)
            {
                var questionView = new QuestionView
                {
                    QuestionId = questionIds[rand.Next(questionIds.Count)],
                    UserId = UserIds[rand.Next(UserIds.Count)]
                };
                while (questionViews.Contains(questionView))
                {
                    questionView = new QuestionView
                    {
                        QuestionId = questionIds[rand.Next(questionIds.Count)],
                        UserId = UserIds[rand.Next(UserIds.Count)]
                    };
                }
                if (i % 10 == 0)
                {
                    Console.WriteLine();
                }
                questionViews.Add(questionView);
            }
            foreach (QuestionView qv in questionViews)
            {
                context.QuestionViews.Add(qv);
            }

            context.SaveChanges();

            /***********************************************************************
            * QuestionFlags
            ***********************************************************************/

            /***********************************************************************
            * QuestionVotes
            ***********************************************************************/
            var questionVotes = new HashSet<Vote>();

            for (int i = 0; i < totalVotes; i++)
            {
                var randomNumber = rand.Next(2);
                var voteTypeIndicator = rand.Next(10);
                var vote = new Vote
                {
                    PostId = randomNumber > 0 ? questionIds[rand.Next(numOfQuestions)] : answerIds[rand.Next(numOfAnswers)],
                    VoterId = UserIds[rand.Next(UserIds.Count)],
                    VoteType = voteTypeIndicator > 0 ? 1 : -1
                };
                while (questionVotes.Contains(vote))
                {
                    vote = new Vote
                    {
                        PostId = randomNumber > 0 ? questionIds[rand.Next(numOfQuestions)] : answerIds[rand.Next(numOfAnswers)],
                        VoterId = UserIds[rand.Next(UserIds.Count)],
                        VoteType = voteTypeIndicator > 0 ? 1 : -1
                    };
                }
                questionVotes.Add(vote);

                var postId = String.Copy(vote.PostId);
                vote.PostId = null;
                var voter = context.Set<ApplicationUser>().Where(u => u.Id.Equals(vote.VoterId)).SingleOrDefault();
                if (randomNumber > 0)
                {
                    var question = context.Set<Question>().Where(q => q.Id.Equals(postId))
                        .Include(q => q.Publisher)
                        .Include(q => q.Votes)
                        .SingleOrDefault();
                    question.Votes.Add(vote);
                    if (voteTypeIndicator > 0)
                    {
                        question.SumVotes++;
                        question.Publisher.Points += Constants.QUESTION_UPVOTE_ASKER_BONUS;
                    }
                    else
                    {
                        question.SumVotes--;
                        question.Publisher.Points += Constants.QUESTION_DOWNVOTE_AKSER_PENALTY;
                        if (voter.Id.Equals(question.PublisherId))
                        {
                            question.Publisher.Points += Constants.QUESTION_DOWNVOTE_VOTER_PENALTY;
                        }
                        else
                        {
                            voter.Points += Constants.QUESTION_DOWNVOTE_VOTER_PENALTY;
                        }
                    }
                    context.Update(question);
                    context.SaveChanges();
                }
                else
                {
                    var answer = context.Set<Answer>()
                        .Where(a => a.Id.Equals(postId))
                        .Include(a => a.Publisher)
                        .Include(a => a.Votes)
                        .SingleOrDefault();
                    answer.Votes.Add(vote);
                    if (voteTypeIndicator > 0)
                    {
                        answer.SumVotes++;
                        answer.Publisher.Points += Constants.ANSWER_UPVOTE_ANSWERER_BONUS;
                    }
                    else
                    {
                        answer.SumVotes--;
                        answer.Publisher.Points += Constants.ANSWER_DOWNVOTE_ANSWERER_PNEALTY;
                        if (voter.Id.Equals(answer.PublisherId))
                        {
                            answer.Publisher.Points += Constants.ANSWER_DOWNVOTE_VOTER_PENALTY;
                        }
                        else
                        {
                            voter.Points += Constants.ANSWER_DOWNVOTE_VOTER_PENALTY;
                        }
                    }
                    context.Update(answer);
                    context.SaveChanges();
                }
            }
            //context.SaveChanges();
            foreach (Vote v in questionVotes)
            {
                context.Votes.Add(v);
            }

            context.SaveChanges();

            /***********************************************************************
            * DeleteVotes
            ***********************************************************************/
        }

        private static List<String> GetUserRole(int points)
        {
            List<string> roles = new List<string>();
            if (15 <= points)
            {
                roles.Append(Constants.VOTER);
            }
            if (500 <= points)
            {
                roles.Append(Constants.EDITOR);
            }
            if (3000 <= points)
            {
                roles.Append(Constants.MODERATOR);
            }
            return roles;
        }
    }
}
