using System;
using System.Web.Mvc;
using FunWithStore.WebUI.HtmlHelpers;
using FunWithStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunWithStore.Tests.HtmlHelpersTests
{
    [TestClass]
    public class PagingHelper
    {
        [TestMethod]
        public void PageLinks_AddedLinks_ShouldBeLinksInHtml()
        {
            //arrange
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            //assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }
    }
}
