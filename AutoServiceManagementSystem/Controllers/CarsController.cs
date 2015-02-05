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

namespace AutoServiceManagementSystem.Controllers
{
	public class CarsController : Controller
	{
		private ICarRepository carRepo;

		public CarsController()
		{
			this.carRepo = new CarRepository(new ASMSContext());
		}

		// GET: Cars
		public ActionResult Index()
		{
			return View(carRepo.GetCars());
		}

		// GET: Cars/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Car car = carRepo.GetCarById(id);

			if (car == null)
			{
				return HttpNotFound();
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
		public ActionResult Create([Bind(Include = "CarId,Manufacturer,Model,PlateCode,VIN,Year,Mileage,Displacement,FuelType")] Car car)
		{
			if (ModelState.IsValid)
			{
				carRepo.InsertCar(car);
				carRepo.Save();
				return RedirectToAction("Index");
			}

			return View(car);
		}

		// GET: Cars/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Car car = carRepo.GetCarById(id);

			if (car == null)
			{
				return HttpNotFound();
			}
			return View(car);
		}

		// POST: Cars/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "CarId,Manufacturer,Model,PlateCode,VIN,Year,Mileage,Displacement,FuelType")] Car car)
		{
			if (ModelState.IsValid)
			{
				carRepo.UpdateCar(car);
				carRepo.Save();
				return RedirectToAction("Index");
			}
			return View(car);
		}

		// GET: Cars/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Car car = carRepo.GetCarById(id);

			if (car == null)
			{
				return HttpNotFound();
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
