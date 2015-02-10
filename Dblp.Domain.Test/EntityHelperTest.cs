//using System;
//using System.Xml.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
////using Dblp.Data.Helper;

//namespace Dblp.Domain.Test
//{
//    [TestClass]
//    public class EntityHelperTest
//    {
//        [TestMethod]
//        public void ToPersonTest_GivenValidXElement_ReturnValidPerson()
//        {
//            //Arrange
//            const string xElementString = @"<www mdate=""2014-09-17"" key=""homepages/150/9544"">
//                            <author>Byungjoon Kim</author>
//                            <title>Home Page</title>
//                            </www>";
//            var target = XElement.Parse(xElementString);
//            //Act
//            var actual = target.ToPerson();
//            //Assert
//            Assert.IsNotNull(actual);
            
//            Assert.IsNotNull(actual.Mdate);
//            Assert.IsNotNull(actual.Key);
//            Assert.IsNotNull(actual.Names);
//            Assert.IsNotNull(actual.Url);

//            Assert.AreEqual(1,actual.Names.Count);
//            Assert.AreEqual(0,actual.Url.Count);

//            Assert.AreEqual("Byungjoon Kim",actual.Names.FirstOrDefault());
//            Assert.AreEqual("homepages/150/9544", actual.Key);
//            Assert.AreEqual(new DateTime(2014,9,17), actual.Mdate);

//        }
//        [TestMethod]
//        public void ToPersonTest_GivenValidXElementWithUrl_ReturnValidPerson()
//        {
//            //Arrange
//            const string xElementString = @"<www mdate=""2014-08-29"" key=""homepages/150/0992"">
//<author>James B. Collins</author>
//<title>Home Page</title>
//<url>http://www.math.colostate.edu/~collins/</url>
//<note type=""affiliation"">Colorado State University, Department of Mathematics</note>
//</www>";
//            var target = XElement.Parse(xElementString);
//            //Act
//            var actual = target.ToPerson();
//            //Assert
//            Assert.IsNotNull(actual);

//            Assert.IsNotNull(actual.Mdate);
//            Assert.IsNotNull(actual.Key);
//            Assert.IsNotNull(actual.Names);
//            Assert.IsNotNull(actual.Url);
//            Assert.IsNotNull(actual.Notes);

//            Assert.AreEqual(1, actual.Names.Count);
//            Assert.AreEqual(1, actual.Url.Count);
//            Assert.AreEqual(1, actual.Notes.Count);

//            Assert.AreEqual("James B. Collins", actual.Names.FirstOrDefault());
//            Assert.AreEqual("http://www.math.colostate.edu/~collins/", actual.Url.FirstOrDefault());
//            Assert.AreEqual("Colorado State University, Department of Mathematics", actual.Notes["affiliation"]);
            
//            Assert.AreEqual("homepages/150/0992", actual.Key);
//            Assert.AreEqual(new DateTime(2014, 8, 29), actual.Mdate);

//        }
//        [TestMethod,Ignore]
//        public void ToPersonTest_GivenValidXElementWithUnicodeName_ReturnValidPerson()
//        {
//            //Arrange
//            const string xElementString = @"<www mdate=""2014-09-17"" key=""homepages/150/9544"">
//                            <author>NicolÃ¡s Sirolli</author>
//                            <title>Home Page</title>
//                            </www>";
//            var target = XElement.Parse(xElementString);
//            //Act
//            var actual = target.ToPerson();
//            //Assert
//            Assert.IsNotNull(actual);

//            Assert.IsNotNull(actual.Mdate);
//            Assert.IsNotNull(actual.Key);
//            Assert.IsNotNull(actual.Names);
//            Assert.IsNotNull(actual.Url);

//            Assert.AreEqual(1, actual.Names.Count);
//            Assert.AreEqual(0, actual.Url.Count);

//            Assert.AreEqual("Nicolás Sirolli", actual.Names.FirstOrDefault());
//            Assert.AreEqual("homepages/150/9544", actual.Key);
//            Assert.AreEqual(new DateTime(2014, 9, 17), actual.Mdate);

//        }
//    }
//}
