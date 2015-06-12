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
			bundles.Add(new ScriptBundle("~/js/jquery").Include(
						"~/Scripts/Vendor/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/js/jqueryval").Include(
						"~/Scripts/Vendor/jquery.validate*",
						"~/Scripts/App/jquery.validate.fixes.js",
						"~/Scripts/App/jquery.validate.hooks.js"));

			bundles.Add(new ScriptBundle("~/js/ajax").Include(
						"~/Scripts/Vendor/jquery.unobtrusive-ajax*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/js/modernizr").Include(
						"~/Scripts/Vendor/modernizr-*"));

			bundles.Add(new ScriptBundle("~/js/bootstrap").Include(
					  "~/Scripts/Vendor/bootstrap.js",
					  "~/Scripts/Vendor/respond.js"));

			bundles.Add(new ScriptBundle("~/js/autocomplete").Include(
						"~/Scripts/Vendor/autocomplete.min.js"));

			bundles.Add(new ScriptBundle("~/js/skype").Include(
						"~/Scripts/Vendor/skype-uri.js"));

			// Application scripts
			bundles.Add(new ScriptBundle("~/js/app").Include(
					"~/Scripts/App/app.js"));

			bundles.Add(new ScriptBundle("~/js/app/job/add").Include(
					"~/Scripts/App/Job/AddJob.js"));

			bundles.Add(new ScriptBundle("~/js/app/job/edit").Include(
					"~/Scripts/App/Job/EditJob.js"));

			bundles.Add(new ScriptBundle("~/js/app/supplier/index").Include(
					"~/Scripts/App/Supplier/Index.js"));


			// Stylesheets
			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/Styles/bootstrap.min.css",
					  "~/Content/Styles/font-awesome.min.css",
					  "~/Content/Styles/site.css"));

			BundleTable.EnableOptimizations = true;
		}
	}
}
