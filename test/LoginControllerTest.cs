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
    /// <summary>
    /// Tests for the Login Controller. 
    /// </summary>
    public class LoginControllerTest
    {

        /// <summary>
        /// Tests the index route for the login screen. It should
        /// render the index view. 
        /// </summary>
        [Fact]
        public void TestIndex()
        {
            var controller = new LoginController();
            var result = controller.Index() as ViewResult;
            Assert.Equal("Index", result.ViewName);
        }

        /// <summary>
        /// Tests the index view when there is no user available.
        /// Similarly, it should render the index view. 
        /// </summary>
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
