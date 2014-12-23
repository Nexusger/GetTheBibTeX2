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
        /// Source http://forums.asp.net/p/1702210/4518688.aspx?Re+Quick+question+about+Ajax+ActionLink+and+span
        /// </summary>
        public static MvcHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string text,
            string action,
            string controller,
            string glyhicon
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

            var span = new TagBuilder("span");
            span.AddCssClass("glyphicon " + glyhicon);
            span.Attributes.Add("aria-hidden", "true");
            var t = Guid.NewGuid().ToString();
            li.InnerHtml = htmlHelper.ActionLink(t, action, controller).ToHtmlString();
            
            return MvcHtmlString.Create(li.ToString().Replace(t,span+" "+text));
        }

        public static MvcHtmlString InputWithGlyphicon(
            this HtmlHelper htmlHelper)
        {
            var input = new TagBuilder("input");
            input.Attributes.Add("type","text");
            input.Attributes.Add("placeholder", "LAK, Romero, Analytics,...");
            input.Attributes.Add("autofocus","");
            input.AddCssClass("form-control");
            input.AddCssClass("typeahead");
            var innerSpan = new TagBuilder("span");
            innerSpan.AddCssClass("glyphicon-search");
            innerSpan.AddCssClass("glyphicon");
            var outerSpan = new TagBuilder("span");
            outerSpan.AddCssClass("input-group-addon");
            var div = new TagBuilder("div");
            div.AddCssClass("input-group-lg");
            div.AddCssClass("input-group");
            div.Attributes.Add("id","search");
            outerSpan.InnerHtml = innerSpan.ToString();
            div.InnerHtml = outerSpan.ToString() +" "+ input;

            return MvcHtmlString.Create(div.ToString());
        }
    }
}