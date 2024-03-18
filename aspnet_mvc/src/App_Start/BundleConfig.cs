using System.Web;
using System.Web.Optimization;

namespace north
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryandui").Include(
                    "~/Scripts/jquery-2.0.3.min.js",
                    "~/Scripts/jquery-ui-1.10.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.validate.min.js",
                    "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(
                    "~/Scripts/i18n/grid.locale-en.min.js",
                    "~/Scripts/jquery.jqGrid.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.min.js",
                    "~/Scripts/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/bootstrap.min.css",
                    "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/cssanduitheme").Include(
                    "~/Content/Site.css",
                    "~/Content/themes/redmond/jquery-ui.min.css",
                    "~/Content/themes/redmond/jquery.ui.theme.min.css"));
        }
    }
}
