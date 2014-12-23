using System.Web.Optimization;
using Dblp.WebUi;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(TypeaheadBundleConfig), "RegisterBundles")]

namespace Dblp.WebUi
{
	public class TypeaheadBundleConfig
	{
		public static void RegisterBundles()
		{
			// Add @Scripts.Render("~/bundles/typeahead") after jQuery in your _Layout.cshtml view
			// When <compilation debug="true" />, MVC 5 will render the full readable version. When set to <compilation debug="false" />, the minified version will be rendered automatically
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/typeahead").Include("~/Scripts/typeahead.bundle*"));
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/typeahead-bloodhound").Include("~/Scripts/bloodhound*"));
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/typeahead-jquery").Include("~/Scripts/typeahead.jquery*"));
		}
	}
}
