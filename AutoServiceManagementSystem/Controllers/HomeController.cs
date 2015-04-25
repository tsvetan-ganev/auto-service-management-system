using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoServiceManagementSystem.Models;
using AutoServiceManagementSystem.DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace AutoServiceManagementSystem.Controllers
{
	public class HomeController : Controller
	{
        private ISupplierRepository suppliersRepo;
        private ApplicationUserManager manager;

        public HomeController()
        {
            var context = new MyDbContext();
            this.suppliersRepo = new SupplierRepository(context);
            var store = new UserStore<ApplicationUser>(context);
            store.AutoSaveChanges = false;
            this.manager = new ApplicationUserManager(store);
        }
        
		public ActionResult Index()
		{
			return View("Landing");
		}

        /* AJAX TEST BEGIN */
        public ActionResult DailySupplier()
        {
            var supplier = GetDailySupplier();
            return PartialView("_DailySupplier", supplier);
        }

        private Supplier GetDailySupplier()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var supplier = suppliersRepo.GetSuppliers()
                .Where(s => s.User == currentUser)
                .OrderBy(s => System.Guid.NewGuid())
                .FirstOrDefault();
            
            return supplier;
        }
        /* AJAX TEST END */

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}