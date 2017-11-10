using System.Web;
using System.Web.Optimization;

namespace Anuitex.AngularLibrary
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

                 bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/Scripts/angular.js",
                      "~/Scripts/App/modules.js",
                      "~/Scripts/App/Services/booksService.js",
                      "~/Scripts/App/Services/journalsService.js",
                      "~/Scripts/App/Services/newspapersService.js",                      
                      "~/Scripts/App/Services/accountService.js",
                      "~/Scripts/App/Services/sellService.js",
                      "~/Scripts/App/Controllers/booksController.js",
                      "~/Scripts/App/Controllers/journalsController.js",
                      "~/Scripts/App/Controllers/newspapersController.js",
                      "~/Scripts/App/Controllers/accountController.js",
                      "~/Scripts/App/Controllers/sellController.js",
                      "~/Scripts/App/directives.js",
                      "~/Scripts/respond.js",
                     "~/Scripts/ngDialog.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
            "~/Content/Styles/Site.css",
            "~/Content/Styles/Icons.css",
            "~/Content/Styles/select2.css",
                "~/Content/Styles/ngDialog.min.css",
                "~/Content/Styles/ngDialog-theme-flat.css"));
        }
    }
}
