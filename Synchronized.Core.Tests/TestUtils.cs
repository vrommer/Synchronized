using Moq;
using Synchronized.Core.Factories;

using System;
using System.Text;
using Synchronized.Domain;
using System.Collections.Generic;
using Synchronized.Model;
using SharedLib.Infrastructure.Constants;

namespace Synchronized.Core.Tests
{
    class TestUtils
    {
        /*****************************************************
        * Types required for the constructor of ModelFactory
        * ----------------------------------------------------
        * StringBuilder
        * IServiceProvider
        ******************************************************/
        public static ModelFactory GetModelFactory()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(s => s.GetService(typeof(ServiceModel.Question))).Returns(new ServiceModel.Question());
            serviceProvider.Setup(s => s.GetService(typeof(ServiceModel.Answer))).Returns(new ServiceModel.Answer());
            serviceProvider.Setup(s => s.GetService(typeof(ServiceModel.PostFlag))).Returns(new ServiceModel.PostFlag());
            serviceProvider.Setup(s => s.GetService(typeof(List<ServiceModel.Answer>))).Returns(new List<ServiceModel.Answer>());
            serviceProvider.Setup(s => s.GetService(typeof(List<ServiceModel.PostFlag>))).Returns(new List<ServiceModel.PostFlag>());
            serviceProvider.Setup(s => s.GetService(typeof(ServiceModel.Comment))).Returns(new ServiceModel.Comment());
            return new ModelFactory();
        }

        public static Question GetQuestionDomainModel()
        {
            string id = "TestQuestionId";
            var question = new Model.Question
            {
                Id = id,
                Title = "The title of the Question",
                Body = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
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
                PublisherId = "TestPublisherId",
                DatePosted = DateTime.Now,
                // Domain.Post
                Publisher = GetUserDoaminModel(),
                PostFlags = GetPostFlags(id),
                DeleteVotes = GetQuestionDeleteVotes(id),
                // Domain.VotedPost
                Comments = GetComments(id),
                Votes = GetVotes(id)
            };

            question.QuestionTags = GetQuestionTags(id, question);
            question.QuestionViews = GetQuestionViews(id, question);
            question.Answers = GetAnswers(id, question);

            return question;
        }

        public static Model.Answer GetAnswerDomainModel(string questionId, Model.Question question)
        {
            string id = "testAnswerId";
            string publisherId = "testUserId";
            Model.Answer a = new Model.Answer
            {
                IsAccepted = false,
                Points = 50,
                PublisherId = publisherId,
                QuestionId = questionId,
                Question = question,
                // Domain.Post
                Id = id,
                Publisher = GetUserDoaminModel(),
                PostFlags = GetPostFlags(id),
                DeleteVotes = GetQuestionDeleteVotes(id),
                Body = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
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
                DatePosted = DateTime.Now,
                // Domain.VotedPost
                Comments = GetComments(id),
                Votes = GetVotes(id)
            };
            return a;
        }

        public static Model.Comment GetCommentDomainMode()
        {
            int randomNumber = (new Random()).Next(2);
            string id = "testCommentId";
            var comment = new Model.Comment
            {
                PostId = randomNumber == 0 ? "testQuestionId": "testAnswerId",
                // Domain.Post
                Id = id,
                Publisher = GetUserDoaminModel(),
                PostFlags = GetPostFlags(id),
                DeleteVotes = GetQuestionDeleteVotes(id),
                Body = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                    "Fusce massa libero, hendrerit non risus a, sollicitudin venenatis tellus. Cras in enim lectus. " +
                    "Nunc nisi metus, condimentum vel urna eget, rutrum feugiat est. Curabitur eget dui eu sapien " +
                    "dictum vulputate vitae eu diam. Suspendisse iaculis, lorem in semper pharetra, felis dui ultrices " +
                    "massa, volutpat rutrum sem nisi nec velit. Donec interdum convallis massa at interdum. Mauris luctus " +
                    "tellus arcu, vitae porttitor leo finibus sed.",
                DatePosted = DateTime.Now,
            };
            return comment;
        }

        public static Tag GetTagDomainModel()
        {
            return new Tag
            {
                DateAdded = DateTime.Now,
                Description = "Test description",
                Id = "testTagId",
                Name = "testTagName",
                Publisher = GetUserDoaminModel(),
                PublisherId = "testUserId"                 
            };
        }

        public static ApplicationUser GetUserDoaminModel()
        {
            return new ApplicationUser
            {
                Id = "testUserId",
                Email = "mail1@example.com"
            };
        }

        public static ICollection<Domain.QuestionView> GetQuestionViews(string id, Model.Question question)
        {
            List<Domain.QuestionView> questionViews = new List<Domain.QuestionView>();
            for (int i = 0; i<(new Random()).Next(20); i++)
            {
                questionViews.Add(new Domain.QuestionView {
                    QuestionId = id,
                    Question = question,
                    UserId = "testUserId",
                    User = GetUserDoaminModel()
                });
            }
            return questionViews;
        }

        public static ICollection<QuestionTag> GetQuestionTags(string id, Model.Question quesiton)
        {
            List<QuestionTag> questionTags = new List<QuestionTag>();
            for (int i = 0; i < (new Random()).Next(20); i++)
            {
                questionTags.Add(new QuestionTag
                {
                    QuestionId = id,
                    Question = quesiton,
                    TagId = "testTagId",
                    Tag = GetTagDomainModel()
                });
            }
            return questionTags;
        }

        public static ICollection<Model.Answer> GetAnswers(string id, Model.Question question)
        {
            List<Model.Answer> answers = new List<Model.Answer>();
            for (int i = 0; i < (new Random()).Next(20); i++)
            {
                answers.Add(GetAnswerDomainModel(id, question));
            }
            return answers;
        }

        public static ICollection<Model.Vote> GetVotes(string id)
        {
            List<Model.Vote> questionVotes = new List<Model.Vote>();
            for (int i = 0; i < (new Random()).Next(20); i++)
            {
                questionVotes.Add(new Model.Vote {
                    PostId = id,
                    Voter = GetUserDoaminModel(),
                    VoterId = "testUserId",
                    VoteType = (new Random()).Next(2) == 0 ? (int)VoteType.UpVote : (int)VoteType.DownVote                     
                });
            }
            return questionVotes;
        }

        public static ICollection<Model.Comment> GetComments(string id)
        {
            List<Model.Comment> comments = new List<Model.Comment>();
            for (int i = 0; i < (new Random()).Next(20); i++)
            {
                comments.Add(GetCommentDomainMode());
            }
            return comments;
        }

        public static ICollection<DeleteVote> GetQuestionDeleteVotes(string id)
        {
            List<DeleteVote> deleteVotest = new List<DeleteVote>();
            for (int i = 0; i < (new Random()).Next(20); i++)
            {
                deleteVotest.Add(new DeleteVote {
                    PostId = "testQuestionId",
                    User = GetUserDoaminModel(),
                    UserId = "testUserId" 
                });
            }
            return deleteVotest;
        }

        public static ICollection<Domain.PostFlag> GetPostFlags(string id)
        {
            List<Domain.PostFlag> postFlags = new List<Domain.PostFlag>();
            for (int i = 0; i < (new Random()).Next(20); i++)
            {
                postFlags.Add(new Domain.PostFlag
                {
                    PostId = id,
                    User = GetUserDoaminModel(),
                    UserId = "testUserId"
                }); 
            }
            return postFlags;
        }
    }
}
