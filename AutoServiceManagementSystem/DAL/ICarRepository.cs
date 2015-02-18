using System;
using System.Collections.Generic;
using System.Linq;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.DAL
{
	public interface ICarRepository : IDisposable
	{
		IEnumerable<Car> GetCars();
        IEnumerable<Car> GetCarsByCustomer(int id);
		Car GetCarById(int? carId);
        Car GetCarByCustomerId(int? carId, int? customerId);
		void InsertCar(Car car);
		void DeleteCar(int carId);
		void UpdateCar(Car car);
		void Save();
	}
}