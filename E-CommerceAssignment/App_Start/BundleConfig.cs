using System.Web;
using System.Web.Optimization;

namespace E_CommerceAssignment
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            /*Custome*/
            bundles.Add(new ScriptBundle("~/bundles/custome").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/nouislider.js",
                        "~/Scripts/numb.js",
                        "~/Scripts/script.js"));

            bundles.Add(new StyleBundle("~/Content/custome").Include(
                      "~/Content/nouislider.css",
                      "~/Content/textfield.css",
                      "~/Content/checkbox.css",
                      "~/Content/range.slider.css",
                      "~/Content/range.slider.min.css",
                      "~/Content/editviewstyle.css",
                      "~/Content/site.css"));
            /*End*/

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));
        }
    }
}
