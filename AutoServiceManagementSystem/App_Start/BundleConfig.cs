using System.Web;
using System.Web.Optimization;

namespace AutoServiceManagementSystem
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			// Vendor scripts
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

			bundles.Add(new ScriptBundle("~/bundles/skype").Include(
						"~/Scripts/Vendor/skype-uri.js"));

			// Application scripts
			bundles.Add(new ScriptBundle("~/bundless/app").Include(
					"~/Scripts/App/app.js"));
			bundles.Add(new ScriptBundle("~/bundles/app/job/add").Include(
					"~/Scripts/App/Job/AddJob.js"));
			bundles.Add(new ScriptBundle("~/bundles/app/job/edit").Include(
					"~/Scripts/App/Job/EditJob.js"));

			bundles.Add(new ScriptBundle("~/bundles/app/supplier/index").Include(
					"~/Scripts/App/Supplier/Index.js"));


			// Stylesheets
			bundles.Add(new StyleBundle("~/styles").Include(
					  "~/Styles/Bootstrap/bootstrap.min.css",
					  "~/Styles/site.css"));
		}
	}
}
