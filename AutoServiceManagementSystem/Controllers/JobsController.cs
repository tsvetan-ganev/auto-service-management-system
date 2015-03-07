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
    public class JobsController : Controller
    {
        private IJobRepository jobRepo;
        private ICustomerRepository customerRepo;
        private ICarRepository carRepo;
        private ApplicationUserManager manager;

        public JobsController()
        {
            var context = new MyDbContext();
            this.jobRepo = new JobRepository(context);
            this.customerRepo = new CustomerRepository(context);
            this.carRepo = new CarRepository(context);

            var store = new UserStore<ApplicationUser>(context);
            store.AutoSaveChanges = false;
            this.manager = new ApplicationUserManager(store);
        }

        // GET: Jobs
        //[Route("Jobs")]
        //[Route("Jobs/All")]
		//public ActionResult Index()
		//{
		//	var currentUser = manager.FindById(User.Identity.GetUserId());
		//	var jobsList = jobRepo.GetJobs()
		//		.Where(j => j.Car.User == currentUser)
		//		.OrderByDescending(j => j.DateStarted);
		//	return View(jobsList);
		//}

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
            return View();
        }

        // POST: Customers/{customerId}/Cars/{carId}/Jobs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "JobId,Mileage,Description,DateStarted,DateFinished,Finished,Paid,Car,User,Customer")] Job job,
            int customerId, int carId)
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
                job.Car = car;
                job.Customer = customer;
                job.User = currentUser;
                jobRepo.InsertJob(job);
                jobRepo.Save();
                return RedirectToAction("Index");
            }

            return View(job);
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

            return View(job);
        }

        // POST: Customers/{id}/Cars/{carId}/Jobs/Edit/{jobId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,Mileage,Description,DateStarted,DateFinished,Finished,Paid,Car,User,Customer")] Job job,
            int customerId, int carId, int jobId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var customer = customerRepo.GetCustomerById(customerId);
            var car = carRepo.GetCarByCustomerId(customerId, carId);

            if (ModelState.IsValid)
            {
                job.Car = car;
                job.Customer = customer;
                job.User = currentUser;
                jobRepo.UpdateJob(job);
                jobRepo.Save();
                return RedirectToAction("Index");
            }
            return View(job);
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
