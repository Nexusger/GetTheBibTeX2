using System.Collections.Generic;
using System.Linq;
using Dblp.Domain.Abstract;
using Dblp.Domain.Entities;
using Dblp.WebUi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dblp.WebUi.Tests.Controller.WebApi2
{
    [TestClass]
    public class PrefetchControllerTest
    {
        [TestMethod]
        public void PrefetchControllerCtorTest_GivenMockRepository_ThrowsNoException()
        {
            //Arrange
            var mockRepo = new Mock<IDblpRepository>();
            //Act
            var target = new PrefetchController(mockRepo.Object);
        }
        [TestMethod]
        public void PrefetchControllerCtorTest_GivenNull_ThrowsException()
        {
            //Arrange
            //Act
            var target = new PrefetchController(null);
        }
        [TestMethod]
        public void GetSearchResultsTest_GivenIdExistInRepo_ReturnOneEntry()
        {
            //Arrange
            var mockSearchResults = new List<SearchResult>();
            mockSearchResults.Add(new SearchResult("1","Eine Person",0,SearchResultSourceType.Person));
            var mockRepo = new Mock<IDblpRepository>();
            mockRepo.Setup(t => t.SearchResults).Returns(mockSearchResults);
            //Act
            var target = new PrefetchController(mockRepo.Object);
            var actual = target.GetSearchResults("person");
            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1,actual.Count());
            Assert.AreEqual("Eine Person",actual.FirstOrDefault().DisplayText);
        }
        [TestMethod]
        public void GetSearchResultsTest_GivenIdDoesnotExistInRepo_ReturnOneEntry()
        {
            //Arrange
            var mockSearchResults = new List<SearchResult>();
            mockSearchResults.Add(new SearchResult("2", "Ein Paper", 0, SearchResultSourceType.Paper));
            var mockRepo = new Mock<IDblpRepository>();
            mockRepo.Setup(t => t.SearchResults).Returns(mockSearchResults);
            //Act
            var target = new PrefetchController(mockRepo.Object);
            var actual = target.GetSearchResults("Hallo");
            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual("Ein Paper", actual.FirstOrDefault().DisplayText);
        }
        
        [TestMethod]
        public void GetSearchResultsTest_GivenRepoIsEmpty_ReturnEmptyList()
        {
            //Arrange
            var mockSearchResults = new List<SearchResult>();
            var mockRepo = new Mock<IDblpRepository>();
            mockRepo.Setup(t => t.SearchResults).Returns(mockSearchResults);
            //Act
            var target = new PrefetchController(mockRepo.Object);
            var actual = target.GetSearchResults("person");
            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Count());
        }

    }
}
