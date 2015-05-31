using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoServiceManagementSystem.Models;
using AutoServiceManagementSystem.DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using AutoServiceManagementSystem.ViewModels.Customers;
using PagedList;

namespace AutoServiceManagementSystem.Controllers
{
    [Authorize()]
    public class CustomersController : Controller
    {
        #region Private Variables
        private ICustomerRepository customersRepo;
        private ICarRepository carsRepo;
        private ISupplierRepository suppliersRepo;
        private IJobRepository jobsRepo;
        private ISparePartRepository sparePartsRepo;
        private ApplicationUserManager manager;
        #endregion

        public CustomersController()
        {
            var context = new MyDbContext();
            this.customersRepo = new CustomerRepository(context);
            this.carsRepo = new CarRepository(context);
            this.jobsRepo = new JobRepository(context);
            this.sparePartsRepo = new SparePartRepository(context);

            var store = new UserStore<ApplicationUser>(context);
            store.AutoSaveChanges = false;
            this.manager = new ApplicationUserManager(store);
        }

        // GET: Customers
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var customers = customersRepo.GetCustomers()
                .Where(c => c.User == currentUser);

			ViewBag.CurrentSort = sortOrder;
			ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "firstname_asc" : "";

			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			if (!String.IsNullOrEmpty(searchString))
			{
				customers = customers
					.Where(c => c.FirstName.ToLower().Contains(searchString.ToLower()) ||
					c.LastName.ToLower().Contains(searchString.ToLower()));
			}

			switch (sortOrder)
			{
				case "firstname_desc":
					customers = customers.OrderByDescending(c => c.FirstName);
					break;
				case "firstname_asc":
					customers = customers.OrderBy(c => c.FirstName);
					break;
				case "lastname_desc":
					customers = customers.OrderByDescending(c => c.LastName);
					break;
				case "lastname_asc":
					customers = customers.OrderBy(c => c.LastName);
					break;
				default:
					customers = customers.OrderByDescending(c => c.DateAdded);
					break;
			}

			int pageNumber = (page ?? 1);
			int pageSize = 12;

			// populating the viewmodel
			var viewModel = new List<DisplayCustomerViewModel>();
			foreach (var customer in customers)
			{
				viewModel.Add(new DisplayCustomerViewModel() {
					Id = customer.CustomerId,
					FirstName = customer.FirstName,
					LastName = customer.LastName,
					DateAdded = customer.DateAdded,
					PhoneNumber = customer.PhoneNumber,
					CarsCount = customersRepo.GetCustomerCarsCountById(customer.CustomerId)
				});
			}
            return View("Customers", viewModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: Customers/Details/5
        public ActionResult Details(int customerId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());

            Customer customer = customersRepo.GetCustomerById(customerId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            if (customer.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = manager.FindById(User.Identity.GetUserId());
                var customer = new Customer()
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    PhoneNumber = viewModel.PhoneNumber,
					DateAdded = DateTime.Now,
                    User = currentUser
                };
                customersRepo.InsertCustomer(customer);
                customersRepo.Save();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int customerId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            Customer customer = customersRepo.GetCustomerById(customerId);

            if (customer == null || customer.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var viewModel = new EditCustomerViewModel()
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber
            };

            return View(viewModel);
        }

        // POST: Customers/Edit/5  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer()
                {
                    CustomerId = viewModel.CustomerId,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    PhoneNumber = viewModel.PhoneNumber
                };
                customersRepo.UpdateCustomer(customer);
                customersRepo.Save();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int customerId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());

            Customer customer = customersRepo.GetCustomerById(customerId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            if (customer.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int customerId)
        {
            var customer = customersRepo.GetCustomerById(customerId);
            if (customer == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            customer.Cars.ToList().ForEach(c =>
            {
                var car = carsRepo.GetCarByCustomerId(customerId, c.CarId);
                car.Jobs.ToList().ForEach(j =>
                {
                    var job = jobsRepo.GetJobById(customerId, c.CarId, j.JobId);
                    job.SpareParts.ToList().ForEach(sp =>
                    {
                        var sparePart = sparePartsRepo.GetSparePartById(job.JobId, sp.SparePartId);
                        sparePartsRepo.DeleteSparePart(sparePart.SparePartId);
                    });
                    sparePartsRepo.Save();

                    jobsRepo.DeleteJob(job.JobId);
                });
                jobsRepo.Save();

                carsRepo.DeleteCar(c.CarId);
            });

            carsRepo.Save();

            customersRepo.DeleteCustomer(customerId);
            customersRepo.Save();
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                customersRepo.Dispose();
				carsRepo.Dispose();
				sparePartsRepo.Dispose();
				jobsRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
