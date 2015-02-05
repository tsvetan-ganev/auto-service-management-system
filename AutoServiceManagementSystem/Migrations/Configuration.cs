namespace AutoServiceManagementSystem.Migrations
{
	using AutoServiceManagementSystem.Models;
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
			AutomaticMigrationDataLossAllowed = true;
			var migrator = new DbMigrator(this);
			pendingMigrations = migrator.GetPendingMigrations().Any();
        }

        protected override void Seed(AutoServiceManagementSystem.Models.ASMSContext context)
        {
            //  This method will be called after migrating to the latest version.

			var bmwX6 = new Car
			{
				Manufacturer = "BMW",
				Model = "X6",
				FuelType = Car.Fuel.Diesel,
				PlateCode = "P 6666 KH",
				VIN = "5GZCZ43D13S812715",
				Year = 2010,
				Displacement = 3.0f,
			};

			var lada = new Car
			{
				Manufacturer = "Lada",
				Model = "Jigula",
				FuelType = Car.Fuel.Petrol,
				PlateCode = "P 7584 KH",
				VIN = "LDZCZ43D13S812715",
				Year = 1981,
				Displacement = 1.2f
			};

			var mercedes = new Car
			{
				Manufacturer = "Mercedes",
				Model = "S-Klass",
				FuelType = Car.Fuel.Diesel,
				PlateCode = "P 777777 P",
				VIN = "1GTGG25R721773607",
				Year = 2014,
				Displacement = 5.0f
			};

			context.Cars.AddOrUpdate(lada);
			context.Cars.AddOrUpdate(bmwX6);
			context.Cars.AddOrUpdate(mercedes);

			/// <summary>
			/// Seeding a list of sample customers.
			/// </summary>
			

			/// <summary>  
			/// Seeding a list of sample spare parts suppliers
			/// </summary>
			context.Suppliers.Add(new Supplier
			{
				Name = "Euro07",
				City = "Ruse",
				DiscountPercentage = 40.0m,
				WebsiteUrl = "http://euro07.bg"
			});

			context.Suppliers.Add(new Supplier
			{
				Name = "Autoplus",
				City = "Ruse",
				DiscountPercentage = 30.0m,
				WebsiteUrl = "http://autoplus.bg"
			});

			context.Suppliers.Add(new Supplier
			{
				Name = "Kosser",
				City = "Ruse",
				DiscountPercentage = 35.0m,
				WebsiteUrl = "http://kosser.bg"
			});

			context.Suppliers.Add(new Supplier
			{
				Name = "Autokelly",
				City = "Varna",
				DiscountPercentage = 20.0m,
				WebsiteUrl = "http://autokelly.bg"
			});

			context.Suppliers.Add(new Supplier
			{
				Name = "Авто Комерс",
				City = "Ruse",
				DiscountPercentage = 28.5m,
				WebsiteUrl = "http://autocommerce.bg"
			});

			context.SaveChanges();

			base.Seed(context);
        }
    }
}
