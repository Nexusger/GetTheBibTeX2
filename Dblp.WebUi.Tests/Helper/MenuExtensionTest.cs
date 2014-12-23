using System.Web.Mvc;
using System.Web.UI.WebControls;
using Dblp.WebUi.Controllers;
using Dblp.WebUi.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dblp.WebUi.Tests.Helper
{
    [TestClass]
    public class MenuExtensionTest
    {
        [TestMethod]
        public void InputWithGlyphiconTest()
        {
            //Arrange
            HtmlHelper mockHtmlHelper = null;
            #region extendedHtml

            const string expected = @"<div class=""input-group input-group-lg"" id=""search""><span class=""input-group-addon""><span class=""glyphicon glyphicon-search""></span></span> <input autofocus="""" class=""form-control typeahead"" placeholder=""LAK, Romero, Analytics,..."" type=""text""></input></div>";
            #endregion

            //Act
            // ReSharper disable once ExpressionIsAlwaysNull
            var target = mockHtmlHelper.InputWithGlyphicon();
            //Assert
            Assert.AreEqual(expected,target.ToString());
        }
        
    }
}
