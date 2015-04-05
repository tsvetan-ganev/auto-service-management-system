using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoServiceManagementSystem.DAL;
using AutoServiceManagementSystem.DAL.Initializers;
using AutoServiceManagementSystem.Helpers.DataBinders;

namespace AutoServiceManagementSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
		{
			Database.SetInitializer<MyDbContext>(new InitializeIdentity());
			//Database.SetInitializer<MyDbContext>(
			//	new DropCreateDatabaseIfModelChanges<MyDbContext>());
			//Database.SetInitializer<ASMSContext>(
			//	new DropCreateDatabaseIfModelChanges<ASMSContext>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
			ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
        }
    }
}
