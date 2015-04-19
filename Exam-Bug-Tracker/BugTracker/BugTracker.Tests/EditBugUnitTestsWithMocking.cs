using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Routing;
using BugTracker.Data.Models;
using BugTracker.RestServices.Controllers;
using BugTracker.RestServices.Models;
using BugTracker.Tests.Mocks;
using BugTracker.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BugTracker.Tests
{
    [TestClass]
    public class EditBugUnitTestsWithMocking
    {
        [TestMethod]
        public void PatchBug_ShouldReturn200k_And_Message()
        {
            // Arrange
            var dataLayerMock = new BugTrackerMock();
            var bugsMock = dataLayerMock.Bugs;
            bugsMock.Add(new Bug() { Id = 1, Title = "Bugche", Description = "Vzemi toz description", Status = Stutus.Open});
            dataLayerMock.SaveChanges();

            //Act
            var bugController = new BugController(dataLayerMock);
            this.SetupControllerForTesting(bugController, "bugs");
            var editedBug = new BugPatchBindingModel()
            {
                Description = "mofified description",
                Status = "Open",
                Title = "Patched title"
            };
            var httpResponse = bugController.EditBug(1, editedBug)
                .ExecuteAsync(new CancellationToken()).Result;

            var httpResponseGetBug = bugController.GetBug(1)
                .ExecuteAsync(new CancellationToken()).Result;
            var modifiedBugFromDb = httpResponseGetBug.Content.ReadAsAsync<BugModel>().Result;

            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
            
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.AreEqual(modifiedBugFromDb.Title, editedBug.Title);
            Assert.AreEqual(modifiedBugFromDb.Description, editedBug.Description);
            Assert.AreEqual(modifiedBugFromDb.Status, editedBug.Status);
        }

        [TestMethod]
        public void PatchBug_InvalidStatusCode_ShouldReturn400k()
        {
            // Arrange
            var dataLayerMock = new BugTrackerMock();
            var bugsMock = dataLayerMock.Bugs;
            bugsMock.Add(new Bug() { Id = 1, Title = "Bugche", Description = "Vzemi toz description", Status = Stutus.Open });
            dataLayerMock.SaveChanges();

            //Act
            var bugController = new BugController(dataLayerMock);
            this.SetupControllerForTesting(bugController, "bugs");
            var editedBug = new BugPatchBindingModel()
            {
                Description = "mofified description",
                Status = "Blqblq",
                Title = "Patched title"
            };
            var httpResponse = bugController.EditBug(1, editedBug)
                .ExecuteAsync(new CancellationToken()).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }

        [TestMethod]
        public void PatchNonExistingBug_ShouldReturn404()
        {
            // Arrange
            var dataLayerMock = new BugTrackerMock();
            var bugsMock = dataLayerMock.Bugs;
            bugsMock.Add(new Bug() { Id = 1, Title = "Bugche", Description = "Vzemi toz description", Status = Stutus.Open });
            dataLayerMock.SaveChanges();

            //Act
            var bugController = new BugController(dataLayerMock);
            this.SetupControllerForTesting(bugController, "bugs");
            var editedBug = new BugPatchBindingModel()
            {
                Description = "mofified description",
                Status = "Open",
                Title = "Patched title"
            };

            var httpResponse = bugController.EditBug(-1, editedBug)
                .ExecuteAsync(new CancellationToken()).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, httpResponse.StatusCode);
        }

        private void SetupControllerForTesting(ApiController controller, string controllerName)
        {
            string serverUrl = "http://sample-url.com";

            // Setup the Request object of the controller
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(serverUrl)
            };
            controller.Request = request;

            // Setup the configuration of the controller
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            controller.Configuration = config;

            // Apply the routes to the controller
            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary
                {
                    { "controller", controllerName }
                });
        }
    }
}
