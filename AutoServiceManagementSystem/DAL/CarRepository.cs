using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.DAL
{
	public class CarRepository : ICarRepository, IDisposable
	{
		private ASMSContext context;

		public CarRepository(ASMSContext context)
		{
			this.context = context;
		}

		#region ICarRepository Implementation

		public IEnumerable<Car> GetCars()
		{
			return context.Cars.ToList();
		}

		public Car GetCarById(int? carId)
		{
			return context.Cars.Find(carId);
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