using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoServiceManagementSystem.Models;
using AutoServiceManagementSystem.DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using AutoServiceManagementSystem.ViewModels.Home;

namespace AutoServiceManagementSystem.Controllers
{
	public class HomeController : Controller
	{
		private ICustomerRepository customersRepo;
        private ICarRepository carsRepo;
        private ISupplierRepository suppliersRepo;
        private IJobRepository jobsRepo;
		private ISparePartRepository sparePartsRepo;
        private ApplicationUserManager manager;

        public HomeController()
        {
            var context = new MyDbContext();
            this.jobsRepo = new JobRepository(context);
            this.customersRepo = new CustomerRepository(context);
            this.carsRepo = new CarRepository(context);
            this.suppliersRepo = new SupplierRepository(context);
			this.sparePartsRepo = new SparePartRepository(context);

            var store = new UserStore<ApplicationUser>(context);
            store.AutoSaveChanges = false;
            this.manager = new ApplicationUserManager(store);
        }
        
		public ActionResult Index()
		{
			return View("Landing");
		}

		[Authorize()]
		public ActionResult Home()
		{
			var currentUser = manager.FindById(User.Identity.GetUserId());

			var recentCustomers = customersRepo.GetCustomers()
				.Where(c => c.User == currentUser)
				.OrderByDescending(c => c.DateAdded)
				.Take<Customer>(5)
				.ToList();

			var recentActiveTasks = jobsRepo.GetJobs()
				.Where(j => j.User == currentUser && !j.IsFinished)
				.OrderByDescending(j => j.DateStarted)
				.Take<Job>(5)
				.ToList();

			var customersOwingMoney = customersRepo.GetCustomers()
				.Where(c => c.User == currentUser
					&& customersRepo.GetCustomerMoneyOwed(c.CustomerId) > 0)
				.Select(c => new DebtorViewModel
				{
					CustomerId = c.CustomerId,
					Name = (c.FirstName + " " + c.LastName).Trim(),
					MoneyOwed = customersRepo.GetCustomerMoneyOwed(c.CustomerId)
				})
				.OrderByDescending(c => c.MoneyOwed)
				.ToList();

			var model = new HomeViewModel
			{
				RecentCustomers = recentCustomers,
				RecentActiveTasks = recentActiveTasks,
				CustomersOwingMoney = customersOwingMoney
			};

			return View(model);
		}
	}
}