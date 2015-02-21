using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.DAL
{
	public class CarRepository : ICarRepository, IDisposable
	{
		private MyDbContext context;

		public CarRepository(MyDbContext context)
		{
			this.context = context;
		}

		#region ICarRepository Implementation

		public IEnumerable<Car> GetCars()
		{
			return context.Cars.ToList();
		}

        public IEnumerable<Car> GetCarsByCustomer(int id)
        {
            return context.Cars
                .Where(c => c.Customer.CustomerId == id)
                .ToList();
        }

		public Car GetCarById(int? carId)
		{
			return context.Cars.Find(carId);
		}

		public Car GetCarByCustomerId(int? customerId, int? carId)
        {
			var car = context.Cars
				.Include(c => c.Customer)
				.Where(c => c.Customer.CustomerId == customerId)
				.Where(c => c.CarId == carId)
				.First();

            return car;
        }

		public void InsertCar(Car car)
		{
			context.Cars.Add(car);
		}

		public void DeleteCar(int carId)
		{
			Car car = context.Cars.Find(carId);
			context.Cars.Remove(car);
		}

		public void UpdateCar(Car car)
		{
			context.Entry(car).State = EntityState.Modified;
		}

		public void Save()
		{
			context.SaveChanges();
		}

		#endregion

		private bool disposed = false;
		
		public virtual void Dispose(bool disposing)
		{
			if (!disposed && disposing)
			{
				context.Dispose();
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
    }
}