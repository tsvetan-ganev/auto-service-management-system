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
        public ActionResult Index()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var customersList = customersRepo.GetCustomers()
                .Where(c => c.User == currentUser);
            return View(customersList);
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
            }
            base.Dispose(disposing);
        }
    }
}
