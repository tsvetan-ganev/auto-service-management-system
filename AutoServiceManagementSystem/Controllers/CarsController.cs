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
		private ApplicationUserManager manager;

		public CarsController()
		{
			var context = new MyDbContext();
			this.carRepo = new CarRepository(context);
			var store = new UserStore<ApplicationUser>(context);
			store.AutoSaveChanges = false;
			this.manager = new ApplicationUserManager(store);
		}

		// GET: Cars
		public ActionResult Index()
		{
			var currentUser = manager.FindById(User.Identity.GetUserId());
			var carsList = carRepo.GetCars()
				.Where(c => c.User == currentUser);

			return View(carsList);
		}

		// GET: Cars/Details/5
		public ActionResult Details(int? id)
		{
			var currentUser = manager.FindById(User.Identity.GetUserId());
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Car car = carRepo.GetCarById(id);

			if (car == null)
			{
				return HttpNotFound();
			}
			if (car.User != currentUser)
			{
				return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
			}
			return View(car);
		}

		// GET: Cars/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Cars/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "CarId,Manufacturer,Model,PlateCode,VIN,EngineCode,Year,FuelType,User")] Car car)
		{
			var currentUser = manager.FindById(User.Identity.GetUserId());
			if (ModelState.IsValid)
			{
				car.User = currentUser;
				carRepo.InsertCar(car);
				carRepo.Save();
				return RedirectToAction("Index");
			}

			return View(car);
		}

		// GET: Cars/Edit/5
		public ActionResult Edit(int? id)
		{
			var currentUser = manager.FindById(User.Identity.GetUserId());
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Car car = carRepo.GetCarById(id);

			if (car == null)
			{
				return HttpNotFound();
			}
			if (car.User != currentUser)
			{
				return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
			}

			return View(car);
		}

        // POST: Customer/{customerId}/Car/{carId}/Edit
		[HttpPost]
		[ValidateAntiForgeryToken]
        [Route("Customer/{customerId}/Car/{carId}/Edit")]
		public ActionResult Edit([Bind(Include = "CarId,Manufacturer,Model,PlateCode,VIN,EngineCode,Year,FuelType,User,Customer")] Car car,
            int customerId)
		{
			if (ModelState.IsValid)
			{
				carRepo.UpdateCar(car);
				carRepo.Save();
                return RedirectToAction("DisplayAllCarsByCustomer", customerId);
			}
			return View(car);
		}

		// GET: Cars/Delete/5
		public ActionResult Delete(int? id)
		{
			var currentUser = manager.FindById(User.Identity.GetUserId());
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Car car = carRepo.GetCarById(id);

			if (car == null)
			{
				return HttpNotFound();
			}
			if (car.User != currentUser)
			{
				return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
			}
			return View(car);
		}

		// POST: Cars/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			carRepo.DeleteCar(id);
			carRepo.Save();
			return RedirectToAction("Index");
		}

        
        // GET: Customer/{id}/Cars
        [Route("Customer/{id}/Cars")]
        public ActionResult DisplayAllCarsByCustomer(int id)
        {
            //var currentUser = manager.FindById(User.Identity.GetUserId());
            var cars = carRepo.GetCarsByCustomer(id);
            return View(cars);
        }

        // GET: Customer/{customerId}/Car/{carId}/Edit
        [Route("Customer/{customerId}/Car/{carId}/Edit")]
        public ActionResult Edit(int? customerId, int? carId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());

            if (customerId == null || carId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Car car = carRepo.GetCarByCustomerId(carId, customerId);

            if (car == null)
            {
                return HttpNotFound();
            }

            if (car.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            return View(car);
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
