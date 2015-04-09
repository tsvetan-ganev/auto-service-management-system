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

namespace AutoServiceManagementSystem.Controllers
{
    [Authorize()]
    public class CustomersController : Controller
    {
        private ICustomerRepository customersRepo;
        private ICarRepository carsRepo;
        private ISupplierRepository suppliersRepo;
        private IJobRepository jobsRepo;
        private ISparePartRepository sparePartsRepo;
        private ApplicationUserManager manager;

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
            if (customerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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
        public ActionResult Create([Bind(Include = "CustomerId,FirstName,LastName,PhoneNumber,User")] Customer customer)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                customer.User = currentUser;
                customersRepo.InsertCustomer(customer);
                customersRepo.Save();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize()]
        public ActionResult Edit(int customerId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (customerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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

        // POST: Customers/Edit/5  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,FirstName,LastName,PhoneNumber,User")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customersRepo.UpdateCustomer(customer);
                customersRepo.Save();
                return RedirectToAction("Index");
            }
            return View(customer);
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
