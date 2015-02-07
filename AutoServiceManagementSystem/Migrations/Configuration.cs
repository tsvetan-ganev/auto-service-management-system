namespace AutoServiceManagementSystem.Migrations
{
	using AutoServiceManagementSystem.Models;
	using AutoServiceManagementSystem.DAL;
	using AutoServiceManagementSystem.Helpers;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<ASMSContext>
	{
		private readonly bool pendingMigrations;

		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = false;
			var migrator = new DbMigrator(this);
			pendingMigrations = migrator.GetPendingMigrations().Any();
		}

		protected override void Seed(ASMSContext context)
		{
			//  This method will be called after migrating to the latest version.

			// adding cars
			var manufacturers = ManufacturersList.Manufacturers;
			var rand = new Random(DateTime.Now.Millisecond);
			int min = 0;
			int max = manufacturers.Count;

			#region Cars Seed Data
			context.Cars.AddOrUpdate(c => c.CarId,
				new Car
				{
					CarId = 1,
					Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
					EngineCode = "XXYYZZ",
					VIN = "1D8HS58N93F387970",
					FuelType = Car.Fuel.Diesel
				},
				new Car
				{
					CarId = 2,
					Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
					EngineCode = "ZZZYYYXXX",
					VIN = "JTHBC1KSXB5307152",
					FuelType = Car.Fuel.Petrol
				},
				new Car
				{
					CarId = 3,
					Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
					EngineCode = "ZKL",
					VIN = "2D8GP74L03R827449",
					FuelType = Car.Fuel.Petrol
				},
				new Car
				{
					CarId = 4,
					Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
					EngineCode = "AEZM",
					VIN = "SALTW12422A939559",
					FuelType = Car.Fuel.Gas
				},
				new Car
				{
					CarId = 5,
					Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
					EngineCode = "KKL",
					VIN = "1GT121E84BF673442",
					FuelType = Car.Fuel.Gas
				},
				new Car
				{
					CarId = 6,
					Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
					EngineCode = "QWER",
					VIN = "1FTRF14V48K950702",
					FuelType = Car.Fuel.Diesel
				},
				new Car
				{
					CarId = 7,
					Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
					EngineCode = "JGKLT",
					VIN = "WBAEU33492P793624",
					FuelType = Car.Fuel.Diesel
				},
				new Car
				{
					CarId = 8,
					Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
					EngineCode = "NBGJK",
					VIN = "1J8FF48W17D850420",
					FuelType = Car.Fuel.Petrol
				},
				new Car
				{
					CarId = 9,
					Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
					EngineCode = "BGFR",
					VIN = "1FTPW12597F436264",
					FuelType = Car.Fuel.Petrol
				},
				new Car
				{
					CarId = 10,
					Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
					EngineCode = "MKLBG",
					VIN = "1GTHC39R9XF262667",
					FuelType = Car.Fuel.Petrol
				}
			);
			#endregion

			#region Customers Seed Data
			context.Customers.AddOrUpdate(c => c.CustomerId,
				new Customer
				{
					CustomerId = 1,
					FirstName = "Ivan",
					LastName = "Georgiev",
					PhoneNumber = "0816136258"
				},
				new Customer
				{
					CustomerId = 2,
					FirstName = "Georgi Ivanov",
					LastName = "Georgiev",
					PhoneNumber = "0875259779"
				},
				new Customer
				{
					CustomerId = 3,
					FirstName = "Petar",
					LastName = "Petrov",
					PhoneNumber = "0886472046"
				},
				new Customer
				{
					CustomerId = 4,
					FirstName = "Maria",
					LastName = "Ilieva",
					PhoneNumber = "0836140833"
				},
				new Customer
				{
					CustomerId = 5,
					FirstName = "Haralampi",
					LastName = "Karaivanov",
					PhoneNumber = "0853748786"
				},
				new Customer
				{
					CustomerId = 6,
					FirstName = "Hristian",
					LastName = "Georgiev",
					PhoneNumber = "0863933157"
				}, new Customer
				{
					CustomerId = 7,
					FirstName = "Ivan",
					LastName = "Ivanov",
					PhoneNumber = "0819116973"
				}, new Customer
				{
					CustomerId = 8,
					FirstName = "Евлоги",
					LastName = "Христов",
					PhoneNumber = "0847900455"
				}
			);
			#endregion

			#region Suppliers Seed Data
			context.Suppliers.AddOrUpdate(s => s.SupplierId,
				new Supplier
				{
					SupplierId = 1,
					Name = "Euro07",
					DiscountPercentage = 45.0m,
					City = "Rousse",
					WebsiteUrl = "http://euro07.bg"
				},
				new Supplier
				{
					SupplierId = 2,
					Name = "AutoPlus",
					DiscountPercentage = 33.0m,
					City = "Rousse",
					WebsiteUrl = "http://autoplus.bg"
				},
				new Supplier
				{
					SupplierId = 3,
					Name = "AvtoKomers",
					DiscountPercentage = 25.0m,
					City = "Rousse",
					WebsiteUrl = "http://acommers.bg"
				},
				new Supplier
				{
					SupplierId = 4,
					Name = "MegaParts",
					DiscountPercentage = 10.0m,
					City = "Sofia",
					WebsiteUrl = "http://megaparts.bg"
				},
				new Supplier
				{
					SupplierId = 5,
					Name = "Inter Cars",
					DiscountPercentage = 40.0m,
					City = "Rousse",
					WebsiteUrl = "http://intercars.bg"
				}
			);
			#endregion

			context.SaveChanges();

			base.Seed(context);
		}
	}
}
