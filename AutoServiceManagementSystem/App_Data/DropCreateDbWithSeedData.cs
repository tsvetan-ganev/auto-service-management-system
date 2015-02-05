using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.Data
{
    public class DropCreateDbWithSeedData : DropCreateDatabaseAlways<ASMSContext>
    {
        protected override void Seed(ASMSContext context)
        {
            /// <summary>
            /// Seeding a list of sample cars.
            /// </summary>
            var bmwX6 = new Car
            {
                Manufacturer = "BMW",
                Model = "X6",
                FuelType = Car.Fuel.Diesel,
                Mileage = 25000,
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
                Mileage = 999999,
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
                Mileage = 10000,
                PlateCode = "P 777777 P",
                VIN = "1GTGG25R721773607",
                Year = 2014,
                Displacement = 5.0f
            };

            /// <summary>
            /// Seeding a list of sample customers.
            /// </summary>
            context.Customers.Add(new Customer
            {
                FirstName = "Ivan",
                LastName = "Petrov",
                PhoneNumber = "0881234567",
                Cars = { bmwX6, lada }
            });

            context.Customers.Add(new Customer
            {
                FirstName = "Georgi",
                LastName = "Ivanov",
                PhoneNumber = "0881234567",
                Cars = { mercedes }
            });
             
            /// <summary>  
            /// Seeding a list of sample spare parts suppliers
            /// </summary>
            context.Suppliers.Add(new Supplier {
                Name = "Euro07",
                City = "Ruse",
                DiscountPercentage = 40.0m,
                WebsiteUrl = "http://euro07.bg"
            });

            context.Suppliers.Add(new Supplier{
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

            context.Suppliers.Add(new Supplier{
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

            base.Seed(context);
        }
    }
}
