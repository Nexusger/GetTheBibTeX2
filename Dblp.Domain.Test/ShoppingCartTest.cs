using System.Linq;
using Dblp.Domain.Interfaces.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dblp.Domain.Test
{
    [TestClass]
    public class ShoppingCartTest
    {
        [TestMethod]
        public void AddItemTest_GivenOneItemIsAdded_ReturnOneAddedItem()
        {
            //Arrange
            var searchResult = new SearchResult("homepages/r/DawkinsRichard", "Richard Dawkins",0,
                SearchResultSourceType.Person,"");

            var target = new ShoppingCart();
            //Act

            Assert.IsTrue(!target.SearchResults.Any());
            target.AddItem(searchResult);

            //Assert
            Assert.IsTrue(target.SearchResults.Any(s => s.Equals(searchResult)));
            Assert.IsTrue(target.SearchResults.Count()==1);
        }
        [TestMethod]
        public void AddItemTest_GivenOneItemIsAddedTwice_ReturnOneAddedItem()
        {
            //Arrange
            var searchResult = new SearchResult("homepages/r/DawkinsRichard", "Richard Dawkins",0,
                SearchResultSourceType.Person, "");

            var target = new ShoppingCart();
            //Act

            Assert.IsTrue(!target.SearchResults.Any());
            target.AddItem(searchResult);
            target.AddItem(searchResult);

            //Assert
            Assert.IsTrue(target.SearchResults.Any(s => s.Equals(searchResult)));
            Assert.IsTrue(target.SearchResults.Count() == 1);
        }
        [TestMethod]
        public void RemoveItemTest_GivenOneItemIsAddedTwiceAndRemoved_ReturnZeroItems()
        {
            //Arrange
            var searchResult = new SearchResult("homepages/r/DawkinsRichard", "Richard Dawkins",0,
                SearchResultSourceType.Person, "");

            var target = new ShoppingCart();
            //Act

            Assert.IsTrue(!target.SearchResults.Any());
            target.AddItem(searchResult);
            target.RemoveItem(searchResult);

            //Assert
            Assert.IsTrue(!target.SearchResults.Any());
        }

        [TestMethod]
        public void RemoveItemTest_GivenNonExistentItemIsRemoved_ReturnZeroItems()
        {
            //Arrange
            var searchResult = new SearchResult("homepages/r/DawkinsRichard", "Richard Dawkins",0,
                SearchResultSourceType.Person, "");

            var target = new ShoppingCart();
            //Act

            Assert.IsTrue(!target.SearchResults.Any());
            target.RemoveItem(searchResult);

            //Assert
            Assert.IsTrue(!target.SearchResults.Any());
        }

        [TestMethod]
        public void ClearTest_GivenOneItemIsAddedAndCartIsCleared_ReturnZeroItems()
        {
            //Arrange
            var searchResult = new SearchResult("homepages/r/DawkinsRichard", "Richard Dawkins",0,
                SearchResultSourceType.Person, "");

            var target = new ShoppingCart();
            //Act

            Assert.IsTrue(!target.SearchResults.Any());
            target.AddItem(searchResult);
            target.Clear();

            //Assert
            Assert.IsTrue(!target.SearchResults.Any());
        }

    }
}
