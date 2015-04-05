using System.Web;
using System.Web.Optimization;

namespace AutoServiceManagementSystem
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/Vendor/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Vendor/jquery.validate*", 
						"~/Scripts/App/jquery.validate.fixes.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                        "~/Scripts/Vendor/jquery.unobtrusive-ajax*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Vendor/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/Vendor/bootstrap.js",
                      "~/Scripts/Vendor/respond.js"));

			bundles.Add(new StyleBundle("~/styles").Include(
                      "~/Styles/bootstrap.css",
                      "~/Styles/site.css"));
		}
	}
}
