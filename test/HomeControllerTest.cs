using System;
using Xunit;
using warmup_project_teama_web_app.Controllers;
using warmup_project_teama_web_app.Controllers.Adapters;
using warmup_project_teama_web_app.Models;
using warmup_project_teama_web_app.Models.Session;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.AspNetCore.Http;

namespace Tests
{
    /// <summary>
    /// Tests for the Home Controller. 
    /// </summary>
    public class HomeControllerTest
    {
        /// <summary>
        /// Test the index route when there is no user available. It
        /// should reroute to the login screen because no one has
        /// logged in. 
        /// </summary>
        [Fact]
        public void TestIndexWithNoUser()
        {
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();
            mockSession["user"] = default;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            HomeController controller = new HomeController();
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var result = controller.Index() as RedirectToActionResult;
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Login", result.ControllerName);
        }
    }
}
