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
using AutoServiceManagementSystem.ViewModels.Jobs;

namespace AutoServiceManagementSystem.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private IJobRepository jobRepo;
        private ICustomerRepository customerRepo;
        private ICarRepository carRepo;
        private ISupplierRepository supplierRepo;
        private ApplicationUserManager manager;

        public JobsController()
        {
            var context = new MyDbContext();
            this.jobRepo = new JobRepository(context);
            this.customerRepo = new CustomerRepository(context);
            this.carRepo = new CarRepository(context);
            this.supplierRepo = new SupplierRepository(context);

            var store = new UserStore<ApplicationUser>(context);
            store.AutoSaveChanges = false;
            this.manager = new ApplicationUserManager(store);
        }

        #region Private Methods
        private IEnumerable<SelectListItem> GetUserSuppliers()
        {
            var suppliers = supplierRepo.GetSuppliersByUserId(User.Identity.GetUserId())
                .Select(s => new SelectListItem
                {
                    Value = s.SupplierId.ToString(),
                    Text = s.Name
                });

            return new SelectList(suppliers, "Value", "Text");
        }
        #endregion

        // GET: Customers/{customerId}/Cars/{carId}/Jobs
        public ActionResult Index(int customerId, int carId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var customer = customerRepo.GetCustomerById(customerId);
            var car = carRepo.GetCarById(carId);

            if (customer == null || car == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (customer.User != currentUser || car.Customer != customer)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var jobsList = jobRepo.GetJobs(customerId, carId)
                .OrderByDescending(j => j.DateStarted);

            ViewBag.CustomerId = customerId;
            ViewBag.CarId = carId;
            ViewBag.CustomerName = customer.FirstName + " " + customer.LastName;
            ViewBag.CarManufacturer = car.Manufacturer;
            ViewBag.CarPlateNumber = car.PlateCode;

            return View(jobsList);
        }

        // GET: Customers/{customerId}/Cars/{carId}/Jobs/Details/{jobId}
        public ActionResult Details(int customerId, int carId, int jobId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            Job job = jobRepo.GetJobById(customerId, carId, jobId);

            if (job == null)
            {
                return HttpNotFound();
            }

            if (job.Car.User != currentUser || job.Car.CarId != carId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            ViewBag.CustomerId = customerId;
            ViewBag.CarId = carId;
            ViewBag.JobId = jobId;
            
            return View(job);
        }

        // GET: Customers/{customerId}/Cars/{carId}/Jobs/Create
        public ActionResult Create(int customerId, int carId)
        {
            var createJobViewModel = new CreateJobViewModel();
            var suppliers = GetUserSuppliers();
            createJobViewModel.SpareParts = new List<EditSparePartViewModel>(){
                new EditSparePartViewModel(){
                        Suppliers = new UserSuppliersViewModel(){
                        UserSuppliers = suppliers
                    }
                },
                new EditSparePartViewModel(){
                        Suppliers = new UserSuppliersViewModel(){
                        UserSuppliers = suppliers
                    }
                },
				new EditSparePartViewModel(){
                        Suppliers = new UserSuppliersViewModel(){
                        UserSuppliers = suppliers
                    }
                },
                new EditSparePartViewModel(){
                        Suppliers = new UserSuppliersViewModel(){
                        UserSuppliers = suppliers
                    }
                }
            };

            return View(createJobViewModel);
        }

        // POST: Customers/{customerId}/Cars/{carId}/Jobs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateJobViewModel createJobViewModel, int customerId, int carId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var customer = customerRepo.GetCustomerById(customerId);
            var car = carRepo.GetCarById(carId);

            if (customer.User != currentUser || car.Customer != customer)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (ModelState.IsValid)
            {
                var job = new Job();
                var spareParts = createJobViewModel.SpareParts.Select(sp => 
                    new SparePart(){
                        Name = sp.Name,
                        Code = sp.Code,
                        Price = sp.Price,
                        Quantity = sp.Quantity,
                        Supplier = supplierRepo.GetSupplierById(sp.Suppliers.SelectedSupplierId),
                        Job = job
                    }).ToList();
                job.Car = car;
                job.Customer = customer;
                job.User = currentUser;
                job.Mileage = createJobViewModel.Mileage;
                job.Description = createJobViewModel.Description;
                job.DateStarted = DateTime.Now;
                job.Finished = false;
                job.Paid = false;
                job.SpareParts = spareParts;
                jobRepo.InsertJob(job);
                jobRepo.Save();
                return RedirectToAction("Index");
            }

            return View(createJobViewModel);
        }

        // GET: Customers/{id}/Cars/{carId}/Jobs/Edit/{jobId}
        public ActionResult Edit(int customerId, int carId, int jobId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var customer = customerRepo.GetCustomerById(customerId);
            var car = carRepo.GetCarById(carId);
            var job = jobRepo.GetJobById(customerId, carId, jobId);

            if (job == null)
            {
                return HttpNotFound();
            }

            if (car.User != currentUser || job.User != currentUser
                || customer.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (car.Customer != customer || job.Car != car)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var editJobViewModel = new EditJobViewModel()
            {
                Description = job.Description,
                Mileage = job.Mileage,
                Paid = job.Paid,
                Finished = job.Finished,
                SpareParts = job.SpareParts.Select(sp => new EditSparePartViewModel()
                {
                    Name = sp.Name,
                    Code = sp.Code,
                    Price = sp.Price,
                    Quantity = sp.Quantity,
                    Suppliers = new UserSuppliersViewModel() {
                        UserSuppliers = GetUserSuppliers()
                    }
                }).ToList()
            };

            return View(editJobViewModel);
        }

        // POST: Customers/{id}/Cars/{carId}/Jobs/Edit/{jobId}
		// TODO: Fix
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditJobViewModel editJobViewModel, int customerId, int carId, int jobId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var customer = customerRepo.GetCustomerById(customerId);
            var car = carRepo.GetCarByCustomerId(customerId, carId);

            if (ModelState.IsValid)
            {
                var job = new Job();
                job.Mileage = editJobViewModel.Mileage;
                job.Description = editJobViewModel.Description;
                job.Finished = editJobViewModel.Finished;
                job.Paid = editJobViewModel.Paid;
                jobRepo.UpdateJob(job);
                jobRepo.Save();
                return RedirectToAction("Index");
            }
            return View(editJobViewModel);
        }

        // GET: Customers/{id}/Cars/{carId}/Jobs/Delete/{jobId}
        public ActionResult Delete(int customerId, int carId, int jobId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var customer = customerRepo.GetCustomerById(customerId);
            var car = carRepo.GetCarByCustomerId(customerId, carId);
            Job job = jobRepo.GetJobById(customerId, carId, jobId);

            if (job == null)
            {
                return HttpNotFound();
            }

            if (car.Customer != customer || job.Car != car)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(job);
        }

        // POST: Customers/{id}/Cars/{carId}/Jobs/Delete/{jobId}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int customerId, int carId, int jobId)
        {
            jobRepo.DeleteJob(jobId);
            jobRepo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                carRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
