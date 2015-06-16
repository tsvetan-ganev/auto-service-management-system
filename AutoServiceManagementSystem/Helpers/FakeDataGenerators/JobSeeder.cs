using System;
using System.Collections.Generic;
using System.Linq;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.Helpers
{
	public class JobSeeder
	{
		private static Random rand = new Random();

		private static DateTime GenerateDate()
		{
			DateTime dt = new DateTime(2015, rand.Next(1, 13), rand.Next(1, 28));
			return dt;
		}

		private static int GenerateMileage()
		{
			int mileage = 10000 * rand.Next(1, 99);
			return mileage;
		}

		public static Job NextJob(ApplicationUser user = null, Car car = null, Customer customer = null)
		{
			var job = new Job();
			job.User = user;
			job.Car = car;
			job.Customer = customer;
			job.DateStarted = GenerateDate();
			job.LastModified = job.DateStarted;
			job.Mileage = GenerateMileage();
			job.IsPaid = false;
			job.IsFinished = false;
			job.Description = "Lorem ipsum dolor sit amet, maiorum propriae eligendi te eos, eam ad velit verear laoreet. Omnium diceret no pro, prompta expetendis in ius, everti appareat posidonium usu ut. Ius deseruisse consectetuer id.";
			
			for (int i = 0; i < rand.Next(0, 12); i++)
			{
				var supplier = user.Suppliers[rand.Next(0, user.Suppliers.Count)];
				job.SpareParts.Add(SparePartSeeder.NextSparePart(supplier, job));
			}

			return job;
		}
	}
}