using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dblp.Helper.Test
{
    [TestClass]
    public class BibTexHelperTest
    {

        private static string GetXElement()
        {
            return "<article mdate=\"2009-05-26\" key=\"journals/or/GrigoroudisS03\"><author>Evangelos Grigoroudis</author><author>O. Spyridaki</author><title>Derived vs. stated importance in customer satisfaction surveys.</title><pages>229-247</pages><year>2003</year><volume>3</volume><journal>Operational Research</journal><number>3</number><ee>http://dx.doi.org/10.1007/BF02936403</ee><url>db/journals/or/or3.html#GrigoroudisS03</url></article>";
        }
        [TestMethod]
        public void ToBibTexTest()
        {
            //Arrange
            
            //Act
            var target = GetXElement().ToBibTeX();
            //Assert
            Assert.IsTrue(target.Contains("@article{"),"Type is mising");
            Assert.IsTrue(target.Contains("author = {Evangelos Grigoroudis and O. Spyridaki}"),"Author ist falsch");
            Assert.IsTrue(target.Contains("title = {Derived vs. stated importance in customer satisfaction surveys.}"), "title ist falsch");
            Assert.IsTrue(target.Contains("journal = {Operational Research}"), "journal ist falsch");
            Assert.IsTrue(target.Contains("volume = {3}"), "volume ist falsch");
            Assert.IsTrue(target.Contains("number = {3}"),"number ist falsch");
            Assert.IsTrue(target.Contains("pages = {229-247}"),"pages ist falsch");
            Assert.IsTrue(target.Contains("year = {2003}"),"year ist falsch");
            Assert.IsTrue(target.Contains("url = {http://dx.doi.org/10.1007/BF02936403}"),"url ist falsch");
        }

        [TestMethod]
        public void SpecialCharacterTest_GivenOUmlaut_returnEncodedBibTexEntry()
        {
            //Arrange
            var xml =
                "<article mdate=\"2009-05-26\" key=\"journals/or/GrigoroudisS03\"><author>abcdeöÜä</author></article>";
            //Act
            var target = xml.ToBibTeX();
            //Assert
            Assert.IsTrue(target.Contains("abcde{\"{o}}{\"{U}}{\"{a}}"));
        }
    }
}
