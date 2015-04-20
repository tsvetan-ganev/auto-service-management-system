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

        private ICustomerRepository customersRepo;
        private ICarRepository carsRepo;
        private ISupplierRepository suppliersRepo;
        private IJobRepository jobsRepo;
		private ISparePartRepository sparePartsRepo;
        private ApplicationUserManager manager;

        public JobsController()
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

        #region Private Methods
        /// <summary>
        /// Used to generate the dropdown list used in the Create and Edit views.
        /// </summary>
        /// <returns>SelectList of Suppliers</returns>
        private IEnumerable<SelectListItem> GetUserSuppliers()
        {
            var suppliers = suppliersRepo.GetSuppliersByUserId(User.Identity.GetUserId())
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
            var customer = customersRepo.GetCustomerById(customerId);
            var car = carsRepo.GetCarById(carId);

            if (customer == null || car == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (customer.User != currentUser || car.Customer != customer)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var jobsList = jobsRepo.GetJobs(customerId, carId)
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
            Job job = jobsRepo.GetJobById(customerId, carId, jobId);

            if (job == null)
            {
                return HttpNotFound();
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
            var suppliersSelectList = GetUserSuppliers();

			// TODO: Dynamically resizable list of parts
            createJobViewModel.SpareParts = new List<EditSparePartViewModel>(){
                new EditSparePartViewModel(){
                        Suppliers = new UserSuppliersViewModel(){
                        UserSuppliers = suppliersSelectList,
						SelectedSupplierId = int.Parse(suppliersSelectList.FirstOrDefault().Value)
                    }
                },
                new EditSparePartViewModel(){
                        Suppliers = new UserSuppliersViewModel(){
                        UserSuppliers = suppliersSelectList,
						SelectedSupplierId = int.Parse(suppliersSelectList.FirstOrDefault().Value)
                    }
                },
				new EditSparePartViewModel(){
                        Suppliers = new UserSuppliersViewModel(){
                        UserSuppliers = suppliersSelectList,
						SelectedSupplierId = int.Parse(suppliersSelectList.FirstOrDefault().Value)
                    }
                },
            };

            return View(createJobViewModel);
        }

        // POST: Customers/{customerId}/Cars/{carId}/Jobs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateJobViewModel createJobViewModel, int customerId, int carId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var customer = customersRepo.GetCustomerById(customerId);
            var car = carsRepo.GetCarById(carId);

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
                        Supplier = suppliersRepo.GetSupplierById(sp.Suppliers.SelectedSupplierId),
                        Job = job
                    }).ToList();
                job.Car = car;
                job.Customer = customer;
                job.User = currentUser;
                job.Mileage = createJobViewModel.Mileage;
                job.Description = createJobViewModel.Description;
                job.DateStarted = DateTime.Now;
                job.IsFinished = false;
                job.IsPaid = false;
                job.SpareParts = spareParts;
                jobsRepo.InsertJob(job);
                jobsRepo.Save();
                return RedirectToAction("Index");
            }

            return View(createJobViewModel);
        }

        // GET: Customers/{id}/Cars/{carId}/Jobs/Edit/{jobId}
        public ActionResult Edit(int customerId, int carId, int jobId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var customer = customersRepo.GetCustomerById(customerId);
            var car = carsRepo.GetCarById(carId);
            var job = jobsRepo.GetJobById(customerId, carId, jobId);

            if (job == null)
            {
                return HttpNotFound();
            }

            if (car.User != currentUser || job.User != currentUser
                || customer.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (car.Customer != customer || job.Car != car)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var editJobViewModel = new EditJobViewModel()
            {
                Description = job.Description,
                Mileage = job.Mileage,
                Paid = job.IsPaid,
                Finished = job.IsFinished,
                SpareParts = job.SpareParts.Select(sp => new EditSparePartViewModel()
                {
                    Name = sp.Name,
                    Code = sp.Code,
                    Price = sp.Price,
                    Quantity = sp.Quantity,
                    Suppliers = new UserSuppliersViewModel() {
                        UserSuppliers = GetUserSuppliers(),
						SelectedSupplierId = sp.Supplier.SupplierId
                    }
                }).ToList()
            };

            return View(editJobViewModel);
        }

        // POST: Customers/{id}/Cars/{carId}/Jobs/Edit/{jobId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditJobViewModel viewModel, int customerId, int carId, int jobId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var customer = customersRepo.GetCustomerById(customerId);
            var car = carsRepo.GetCarByCustomerId(customerId, carId);

            if (ModelState.IsValid)
            {
				var job = jobsRepo.GetJobById(customerId, carId, jobId);
				job.Mileage = viewModel.Mileage;
				job.Description = viewModel.Description;
				job.IsPaid = viewModel.Paid;
				job.IsFinished = viewModel.Finished;

                int elementsDifference = viewModel.SpareParts.Count - job.SpareParts.Count;
                for (int i = 0; i < elementsDifference; i++)
                {
                    job.SpareParts.Add(new SparePart());
                }
				for (int i = 0; i < viewModel.SpareParts.Count; i++)
				{
					job.SpareParts[i].Name = viewModel.SpareParts[i].Name;
					job.SpareParts[i].Code = viewModel.SpareParts[i].Code;
					job.SpareParts[i].Price = viewModel.SpareParts[i].Price;
					job.SpareParts[i].Quantity = viewModel.SpareParts[i].Quantity;
					job.SpareParts[i].Supplier = suppliersRepo.GetSupplierById(
						viewModel.SpareParts[i].Suppliers.SelectedSupplierId);
				}
                jobsRepo.UpdateJob(job);
                jobsRepo.Save();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Customers/{id}/Cars/{carId}/Jobs/Delete/{jobId}
        public ActionResult Delete(int customerId, int carId, int jobId)
        {
            Job job = jobsRepo.GetJobById(customerId, carId, jobId);

            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }

        // POST: Customers/{id}/Cars/{carId}/Jobs/Delete/{jobId}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int customerId, int carId, int jobId)
        {
            var job = jobsRepo.GetJobById(customerId, carId, jobId);

            // delete the job's children first
			job.SpareParts.ToList().ForEach(sp =>
			{
				var sparePart = sparePartsRepo.GetSparePartById(jobId, sp.SparePartId);
				sparePartsRepo.DeleteSparePart(sparePart.SparePartId);
			});
			sparePartsRepo.Save();

            // delete the job itself
			jobsRepo.DeleteJob(jobId);
			jobsRepo.Save();

            return RedirectToAction("Index");
        }

        [Route("Jobs/AddSparePart")]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam="*")]
        public ActionResult AddSparePart()
        {
            var suppliersSelectList = GetUserSuppliers();
            var sparePart = new EditSparePartViewModel()
            {
                Suppliers = new UserSuppliersViewModel()
                {
                    UserSuppliers = suppliersSelectList,
                    SelectedSupplierId = int.Parse(suppliersSelectList.FirstOrDefault().Value)
                }
            };
            return View(new List<EditSparePartViewModel>() { sparePart });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                carsRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
