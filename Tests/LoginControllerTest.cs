using System;
using Xunit;
using warmup_project_teama_web_app.Controllers;
using warmup_project_teama_web_app.Controllers.Adapters;
using warmup_project_teama_web_app.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.AspNetCore.Http;

namespace Tests
{
    public class LoginControllerTest
    {
        [Fact]
        public void TestIndex()
        {
            var controller = new LoginController();
            var result = controller.Index() as ViewResult;
            Assert.Equal("Index", result.ViewName);
        }

        [Fact]
        public void TestIndexWithInvalidUserViewModel()
        {
           
            // create the mock http context
            var controller = new LoginController();
            var userViewModel = new UserViewModel();
            var result = controller.Index(userViewModel) as ViewResult;
            Assert.Equal("Index", result.ViewName);
        }
    }
}
