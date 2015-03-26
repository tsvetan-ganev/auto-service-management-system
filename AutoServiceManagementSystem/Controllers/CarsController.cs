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
	public class CarsController : Controller
	{
		private ICarRepository carRepo;
		private ICustomerRepository customerRepo;
		private ApplicationUserManager manager;

		public CarsController()
		{
			var context = new MyDbContext();
			this.carRepo = new CarRepository(context);
			this.customerRepo = new CustomerRepository(context);
			var store = new UserStore<ApplicationUser>(context);
			store.AutoSaveChanges = false;
			this.manager = new ApplicationUserManager(store);
		}

		// GET: Cars
		[Route("Cars")]
		[Route("Cars/Index")]
		[Route("Cars/All")]
		public ActionResult Index()
		{
			throw new NotImplementedException();
		}

		// GET: Customers/{customerId}/Cars/Details/{carId}
		public ActionResult Details(int customerId, int carId)
		{
			var currentUser = manager.FindById(User.Identity.GetUserId());

			Car car = carRepo.GetCarByCustomerId(customerId, carId);

			if (car == null)
			{
				return HttpNotFound();
			}

			if (car.User != currentUser)
			{
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
			}

			return View(car);
		}

        // GET: Customers/{customerId}/Cars/Create
		public ActionResult Create(int customerId)
		{
			return View();
		}

        // POST: Customers/{customerId}/Cars/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "CarId,Manufacturer,Model,PlateCode,VIN,EngineCode,Year,FuelType,User,Customer")] Car car,
            int customerId)
		{
			var currentUser = manager.FindById(User.Identity.GetUserId());
			var customer = customerRepo.GetCustomerById(customerId);

			if (customer.User != currentUser)
			{
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
			}

			if (ModelState.IsValid)
			{
				car.User = currentUser;
				car.Customer = customer;
				carRepo.InsertCar(car);
				carRepo.Save();
				return RedirectToAction("CarsByCustomer", customerId);
			}

			return View(car);
		}


        // GET: Customers/{id}/Cars
        [Route("Customers/{id}/Cars")]
        public ActionResult CarsByCustomer(int id)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
			var customer = customerRepo.GetCustomerById(id);

			if (customer == null)
			{
				// no such customer
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			if (customer.User != currentUser)
			{
				// this user tries to access other user's customer data
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			ViewBag.CustomerId = id;
			ViewBag.Title = string.Format("{0} {1}'s cars.",
				customer.FirstName, customer.LastName);
            var cars = carRepo.GetCarsByCustomer(id);
            return View(cars);
        }

		// GET: Customer/{customerId}/Car/{carId}/Edit
		public ActionResult Edit(int customerId, int carId)
		{
			var currentUser = manager.FindById(User.Identity.GetUserId());

			Car car = carRepo.GetCarByCustomerId(customerId, carId);

			if (car == null)
			{
				return HttpNotFound();
			}

			if (car.User != currentUser)
			{
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
			}

			return View(car);
		}

		// POST: Customer/{customerId}/Car/Edit/{carId}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "CarId,Manufacturer,Model,PlateCode,VIN,EngineCode,Year,FuelType,User,Customer")] Car car,
			int customerId, int carId)
		{
			if (ModelState.IsValid)
			{
				carRepo.UpdateCar(car);
				carRepo.Save();
				return RedirectToAction("CarsByCustomer", customerId);
			}
			return View(car);
		}


		// GET: Customers/{customerId}/Cars/Delete/{carId}
		public ActionResult Delete(int customerId, int carId)
		{
			var currentUser = manager.FindById(User.Identity.GetUserId());

			Car car = carRepo.GetCarByCustomerId(customerId, carId);

			if (car == null)
			{
				return HttpNotFound();
			}

			if (car.User != currentUser)
			{
				return new HttpStatusCodeResult(
                    HttpStatusCode.Forbidden);
			}

			return View(car);
		}

		// POST: Customers/{customerId}/Cars/Delete/{carId}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int customerId, int carId)
		{
			carRepo.DeleteCar(carId);
			carRepo.Save();
			return RedirectToAction("CarsByCustomer", customerId);
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
