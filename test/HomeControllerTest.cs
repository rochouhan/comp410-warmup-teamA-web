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
    public class HomeControllerTest
    {
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
