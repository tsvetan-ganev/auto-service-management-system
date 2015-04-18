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
using AutoServiceManagementSystem.ViewModels.Cars;

namespace AutoServiceManagementSystem.Controllers
{
    [Authorize()]
    public class CarsController : Controller
    {
        #region Private variables
        private ICarRepository carsRepo;
        private ICustomerRepository customersRepo;
        private IJobRepository jobsRepo;
        private ISparePartRepository sparePartsRepo;
        private ApplicationUserManager manager;
        #endregion

        public CarsController()
        {
            var context = new MyDbContext();
            this.carsRepo = new CarRepository(context);
            this.customersRepo = new CustomerRepository(context);
            this.jobsRepo = new JobRepository(context);
            this.sparePartsRepo = new SparePartRepository(context);

            var store = new UserStore<ApplicationUser>(context);
            store.AutoSaveChanges = false;
            this.manager = new ApplicationUserManager(store);
        }

        // GET: Cars
        //[Route("Cars")]
        //[Route("Cars/Index")]
        //[Route("Cars/All")]
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        // GET: Customers/{customerId}/Cars/Create
        public ActionResult Create(int customerId)
        {
            ViewBag.customerId = customerId;
            return View();
        }

        // POST: Customers/{customerId}/Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCarViewModel viewModel, int customerId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var customer = customersRepo.GetCustomerById(customerId);

            if (customer == null || customer.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (ModelState.IsValid)
            {
                var car = new Car();
                car.Manufacturer = viewModel.Manufacturer;
                car.Model = viewModel.Model;
                car.VIN = viewModel.VIN;
                car.EngineCode = viewModel.EngineCode;
                car.PlateCode = viewModel.PlateCode;
                car.Year = viewModel.Year;
                car.FuelType = viewModel.FuelType;
                car.User = currentUser;
                car.Customer = customer;
                carsRepo.InsertCar(car);
                carsRepo.Save();
                return RedirectToAction("CarsByCustomer", customerId);
            }

            return View(viewModel);
        }

        // GET: Customers/{id}/Cars
        [Route("Customers/{id}/Cars")]
        public ActionResult CarsByCustomer(int id)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var customer = customersRepo.GetCustomerById(id);

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
            var cars = carsRepo.GetCarsByCustomer(id);
            return View(cars);
        }

        // GET: Customer/{customerId}/Car/{carId}/Edit
        public ActionResult Edit(int customerId, int carId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());

            Car car = carsRepo.GetCarByCustomerId(customerId, carId);

            if (car == null || car.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var viewModel = new EditCarViewModel()
            {
                Manufacturer = car.Manufacturer,
                Model = car.Model,
                VIN = car.VIN,
                PlateCode = car.PlateCode,
                Year = car.Year,
                EngineCode = car.EngineCode,
                FuelType = car.FuelType
            };
            ViewBag.customerId = customerId;

            return View(viewModel);
        }

        // POST: Customer/{customerId}/Car/Edit/{carId}
        // TODO: Add EditCarViewModel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCarViewModel viewModel, int customerId, int carId)
        {
            if (ModelState.IsValid)
            {
                var car = new Car()
                {
                    CarId = carId,
                    Manufacturer = viewModel.Manufacturer,
                    Model = viewModel.Model,
                    VIN = viewModel.VIN,
                    EngineCode = viewModel.EngineCode,
                    PlateCode = viewModel.PlateCode,
                    Year = viewModel.Year,
                    FuelType = viewModel.FuelType
                };
                carsRepo.UpdateCar(car);
                carsRepo.Save();
                return RedirectToAction("CarsByCustomer", customerId);
            }
            return View(viewModel);
        }

        // GET: Customers/{customerId}/Cars/Delete/{carId}
        public ActionResult Delete(int customerId, int carId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());

            Car car = carsRepo.GetCarByCustomerId(customerId, carId);

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
            Car car = carsRepo.GetCarByCustomerId(customerId, carId);

            if (car == null)
            {
                return HttpNotFound();
            }

            // delete car's jobs first
            car.Jobs.ToList().ForEach(j =>
            {
                var job = jobsRepo.GetJobById(customerId, carId, j.JobId);
                // delete the job's children first
                job.SpareParts.ToList().ForEach(sp =>
                {
                    var sparePart = sparePartsRepo.GetSparePartById(job.JobId, sp.SparePartId);
                    sparePartsRepo.DeleteSparePart(sparePart.SparePartId);
                });
                sparePartsRepo.Save();

                //then delete the job itself
                jobsRepo.DeleteJob(job.JobId);
                jobsRepo.Save();
            });

            // then delete the car itself
            carsRepo.DeleteCar(carId);
            carsRepo.Save();

            return RedirectToAction("CarsByCustomer", customerId);
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
