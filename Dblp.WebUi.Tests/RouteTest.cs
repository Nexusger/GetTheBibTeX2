using System;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dblp.WebUi.Tests
{
    [TestClass]
    public class RouteTest
    {
        #region preparation
        private HttpContextBase createHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            //create the mock request
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);
            
            //creare the mock response
            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            //create the mock context, using the request and response
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            //return the mock objekt
            return mockContext.Object;
        }

        private void TestRouteMatch(string url, string controller, string action, object routeProperties = null,
            string httpMethod = "GET")
        {
            //arrange
            var routes = RegisterRoutes();

            //Act
            var result = routes.GetRouteData(createHttpContext(url, httpMethod));

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncommingRouteResult(result,controller,action,routeProperties));
        }

        private void TestRouteGetMatchDefault(string url)
        {
            //arrange
            var routes = RegisterRoutes();

            //Act
            var result = routes.GetRouteData(createHttpContext(url, "GET"));

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Route);

            TestIncommingRouteResult(result, (string)((System.Web.Routing.Route) (result.Route)).Defaults["controller"],
               (string) ((System.Web.Routing.Route) (result.Route)).Defaults["action"]);
        }

        private bool TestIncommingRouteResult(RouteData routeResult, string controller, string action,
            object propertySet = null)
        {
            Func<object, object, bool> valCompare = (v1, v2) =>
            {
                return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            };
            var result = valCompare(routeResult.Values["controller"], controller) &&
                         valCompare(routeResult.Values["action"], action);
            if (propertySet != null)
            {
                var propInfo = propertySet.GetType().GetProperties();
                foreach (var propertyInfo in propInfo)
                {
                    if (
                        !(routeResult.Values.ContainsKey(propertyInfo.Name) &&
                          valCompare(routeResult.Values[propertyInfo.Name], propertyInfo.GetValue(propertySet, null))))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }


        private RouteCollection RegisterRoutes()
        {
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            return routes;
        }
        private void TestrouteFail(string url)
        {
            //arrange
            var routes = RegisterRoutes();
            //act
            var result = routes.GetRouteData(createHttpContext(url));

            //Assert
            Assert.IsTrue(result == null||result.Route==null);
        }
        #endregion
        [TestMethod]
        public void TestIncommingRoutes_GivenTooManySegments_RouteDidNotMatch()
        {
            TestrouteFail("~/Admin/Index/Segment");
        }
        [TestMethod]
        public void TestIncommingRoutes_GivenTooFewSegments_DefaultRouteDidMatch()
        {
            TestRouteGetMatchDefault("~/Admin");
        }
        [TestMethod]
        public void TestIncommingRoutes_GivenValidUrl_RouteDidMatch()
        {
            TestRouteMatch("~/Admin/Index", "Admin", "Index");
        }
        [TestMethod]
        public void TestIncommingRoutes_GivenValidUrl_RouteDidMatch1()
        {
            TestRouteMatch("~/api/Prefetch", "api", "Prefetch");
        }


    }
}
