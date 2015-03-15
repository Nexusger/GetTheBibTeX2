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
        public void ToBibTexTest_GivenXmlWithDifferentAuthorAttributes_AttributesGotMerged()
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
            Assert.IsTrue(target.Contains("}\r\n}"));
        }

        [TestMethod]
        public void SpecialCharacterTest_GivenUmlauts_returnEncodedBibTexEntry()
        {
            //Arrange
            var xml =
                "<article mdate=\"2009-05-26\" key=\"journals/or/GrigoroudisS03\"><author>abcdeöÜä</author></article>";
            //Act
            var target = xml.ToBibTeX();
            //Assert
            Assert.IsTrue(target.Contains("abcde{\"{o}}{\"{U}}{\"{a}}"));
        }
        [TestMethod]
        public void ToBibTexTest_GivenOddNUmberOfAuthors_ReturnMergedAuthors()
        {
            //Arrange
            var xml =
                "<inproceedings key=\"conf/rws/KashikiSYYW13\" mdate=\"2013-04-12\"><author>Kanshiro Kashiki</author><author>Tomoki Sada</author><author>Akira Yamaguchi</author><author>Kosuke Yamazaki</author><author>Shingo Watanabe</author><title>Systematic coexistence scheme for an additional radio system in the operating area of an existing radio communication system.</title><pages>4-6</pages><year>2013</year><booktitle>RWS</booktitle><ee>http://dx.doi.org/10.1109/RWS.2013.6486622</ee><crossref>conf/rws/2013</crossref><url>db/conf/rws/rws2013.html#KashikiSYYW13</url></inproceedings>";
            //Act
            var target = xml.ToBibTeX();
            //Assert
            Assert.IsFalse(target.Contains("author = {Kanshiro Kashiki},"));
        }



    }
}
