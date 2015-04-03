namespace AutoServiceManagementSystem.Tests.Helpers
{
	using System;
	using System.Collections.Generic;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using AutoServiceManagementSystem.Helpers;
	using AutoServiceManagementSystem.Models;
	using System.Diagnostics;
	using System.Text;


	[TestClass]
	public class JobSeederTests
	{
		[TestMethod]
		public void NextJobSeederShouldGenerateValidRandomJobs()
		{
			var jobs = new List<Job>();
			var user = new ApplicationUser(){
				UserName = "test@test.com",
				Suppliers = new List<Supplier>(){
					new Supplier(){
						Name = "Euro 07",
						DiscountPercentage = 45.0m
					},
					new Supplier(){
						Name = "Auto plus",
						DiscountPercentage = 25.0m
					},
					new Supplier(){
						Name = "Autohit",
						DiscountPercentage = 35.0m
					},
				}
			};

			var customer = AutoServiceManagementSystem.Helpers.CustomerSeeder.NextCustomer();
			var car = AutoServiceManagementSystem.Helpers.CarSeeder.NextCar(user, customer);

			for (int i = 0; i < 1000; i++)
			{
				car.Jobs.Add(AutoServiceManagementSystem.Helpers.JobSeeder.NextJob(user, car, customer));
			}

			foreach (var job in car.Jobs)
			{
				StringBuilder sb = new StringBuilder();
				int mileage = job.Mileage;
				DateTime ds = (DateTime) job.DateStarted;
				sb.Append(mileage);
				sb.Append('\n');
				sb.Append(ds.ToShortDateString());
				job.SpareParts.ForEach(sp => sb.Append(sp.Name + ' ' + sp.Price));

				Debug.WriteLine(sb.ToString());

			}
		}
	}
}
