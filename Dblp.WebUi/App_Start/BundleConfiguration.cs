using System.Web.Optimization;

namespace Dblp.WebUi
{
    public class BundleConfiguration
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css/").Include("~/Content/*.css"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/ko").Include("~/Scripts/knockout-{version}.js"));
        }
    }
}