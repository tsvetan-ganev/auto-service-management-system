using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AutoServiceManagementSystem
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

			routes.MapRoute(
				name: "Customers",
				url: "Customers/{action}/{id}",
				defaults: new { controller = "Customers", action = "Details", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "CustomerCars",
				url: "Customers/{customerId}/Cars/{action}/{carId}",
				defaults: new { controller = "Cars", action = "DisplayAllCarsByCustomer", carId = UrlParameter.Optional }
			);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
		}
	}
}
