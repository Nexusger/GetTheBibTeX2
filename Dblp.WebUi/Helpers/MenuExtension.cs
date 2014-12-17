using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Dblp.WebUi.Helpers
{
    public static class MenuExtension
    {
        /// <summary>
        /// Source http://stackoverflow.com/questions/6323021/better-way-to-get-active-page-link-in-mvc-3-razor
        /// </summary>
        public static MvcHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string text,
            string action,
            string controller
            )
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }
            li.InnerHtml = htmlHelper.ActionLink(text, action, controller).ToHtmlString();
            return MvcHtmlString.Create(li.ToString());
        }

        /// <summary>
        /// Source http://stackoverflow.com/questions/6323021/better-way-to-get-active-page-link-in-mvc-3-razor
        /// </summary>
        public static MvcHtmlString MenuItemWithGlyphIcon(
            this HtmlHelper htmlHelper,
            string text,
            string action,
            string controller,
            string glyhicon
            )
        {

            if (!String.IsNullOrEmpty(glyhicon))
            {
                var span = new TagBuilder("span");
                span.AddCssClass(glyhicon);
            }
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }
            if (!String.IsNullOrEmpty(glyhicon))
            {
                var span = new TagBuilder("span");
                span.AddCssClass(glyhicon);
                li.InnerHtml = span + htmlHelper.ActionLink(text, action, controller).ToHtmlString();
            }
            else
            {
                li.InnerHtml = htmlHelper.ActionLink(text, action, controller).ToHtmlString();

            }
            return MvcHtmlString.Create(li.ToString());
        }

    }
}