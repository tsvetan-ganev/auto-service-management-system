using System;
using System.Collections.Generic;
using System.Linq;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.DAL
{
	public interface ICarRepository : IDisposable
	{
		IEnumerable<Car> GetCars();
		Car GetCarById(int carId);
		void InsertCar(Car car);
		void DeleteCar(int carId);
		void UpdateCar(Car car);
		void Save();
	}
}