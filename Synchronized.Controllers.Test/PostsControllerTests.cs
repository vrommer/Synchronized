using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Synchronized.Core.Interfaces;
using Synchronized.Model;
using Synchronized.WebApp.Controllers;
using System;
using Xunit;

namespace Synchronized.Controllers.Test
{
    public class PostsControllerTests
    {
        [Fact]
        public async void TestDeleteSuccess()
        {
            /*********************************************
             * ------------------------------------------
             * PostsController dependencies:
             * ------------------------------------------
             * IQuestionsService questionsService,
             * IPostsService postsService,
             * ILogger<PostsController> logger,
             * UserManager<ApplicationUser> userManager
             *********************************************/

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var mockILogger = new Mock<ILogger<PostsController>>();
            var mockIQuestionsService = new Mock<IQuestionsService>();
            var mockIPostsService = new Mock<IPostsService>();
            var mockUserManager = new FakeUserManager();

            var questionsService = mockIQuestionsService.Object;
            var postsService = mockIPostsService.Object;
            var userManager = mockUserManager;
            var logger = mockILogger.Object;


            var controller = new PostsController(questionsService, postsService, logger, userManager);

            //var result = await controller.Delete(new ServiceModel.Question { });

            //// Arrange
            //var mockRepo = new Mock<IBrainstormSessionRepository>();
            //mockRepo.Setup(repo => repo.ListAsync()).Returns(Task.FromResult(GetTestSessions()));
            //var controller = new HomeController(mockRepo.Object);

            //// Act
            //var result = await controller.Index();

            //// Assert
            //var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
            //    viewResult.ViewData.Model);
            //Assert.Equal(2, model.Count());
        }

    }
}
