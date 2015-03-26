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
                defaults: new { controller = "Customers", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Jobs",
                url: "Customers/{customerId}/Cars/{carId}/Jobs/{action}/{jobId}",
                defaults: new { controller = "Jobs", action = "Index", jobId = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "SpareParts",
            //    url: "Customers/{customerId}/Cars/{carId}/Jobs/{action}/{jobId}/",
            //    defaults: new { controller = "Jobs", action = "Index", jobId = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Cars",
                url: "Customers/{customerId}/Cars/{action}/{carId}",
                defaults: new { controller = "Cars", action = "CarsByCustomer", carId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
