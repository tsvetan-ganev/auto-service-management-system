namespace AutoServiceManagementSystem.Migrations
{
    using AutoServiceManagementSystem.Models;
    using AutoServiceManagementSystem.DAL;
    using AutoServiceManagementSystem.Helpers;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System.Collections;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<MyDbContext>
    {
        private readonly bool pendingMigrations;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            var migrator = new DbMigrator(this);
            pendingMigrations = migrator.GetPendingMigrations().Any();
        }

        protected override void Seed(MyDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            ClearDatabase<MyDbContext>();
            context = new MyDbContext();

            // Seeding ApplicationUsers
            bool exists = context.Users
                .Any(u => u.UserName == "test1@test.com");

            if (!exists)
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "test1@test.com",
                    Email = "test1@test.com",
                    PhoneNumber = "088888888",
                    UserInfo = new UserInfo
                    {
                        FirstName = "Ali",
                        LastName = "Reza",
                        CompanyName = "Turkish Airlines"
                    }
                };

                manager.Create(user, "Password@123");


                //// adding cars
                var manufacturers = ManufacturersList.Manufacturers;
                var rand = new Random(DateTime.Now.Millisecond);
                int min = 0;
                int max = manufacturers.Count;

                #region Cars Seed Data
                var carsToAdd = new List<Car>{
                    new Car
                    {
                        Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
                        EngineCode = "XXYYZZ",
                        VIN = "1D8HS58N93F387970",
                        FuelType = Car.Fuel.Diesel,
                        User = user
                    },
                    new Car
                    {
                        Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
                        EngineCode = "ZZZYYYXXX",
                        VIN = "JTHBC1KSXB5307152",
                        FuelType = Car.Fuel.Petrol,
                        User = user
                    },
                     new Car
                    {
                        Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
                        EngineCode = "ZKL",
                        VIN = "2D8GP74L03R827449",
                        FuelType = Car.Fuel.Petrol,
                        User = user
                    },
                    new Car
                    {
                        Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
                        EngineCode = "AEZM",
                        VIN = "SALTW12422A939559",
                        FuelType = Car.Fuel.Gas,
                        User = user
                    },
                    new Car
                    {
                        Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
                        EngineCode = "KKL",
                        VIN = "1GT121E84BF673442",
                        FuelType = Car.Fuel.Gas,
                        User = user
                    },
                    new Car
                    {
                        Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
                        EngineCode = "QWER",
                        VIN = "1FTRF14V48K950702",
                        FuelType = Car.Fuel.Diesel
                    },
                    new Car
                    {
                        Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
                        EngineCode = "JGKLT",
                        VIN = "WBAEU33492P793624",
                        FuelType = Car.Fuel.Diesel,
                        User = user
                    },
                    new Car
                    {
                        Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
                        EngineCode = "NBGJK",
                        VIN = "1J8FF48W17D850420",
                        FuelType = Car.Fuel.Petrol
                    },
                    new Car
                    {
                        Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
                        EngineCode = "BGFR",
                        VIN = "1FTPW12597F436264",
                        FuelType = Car.Fuel.Petrol,
                        User = user
                    },
                    new Car
                    {
                        Manufacturer = manufacturers.ElementAt(rand.Next(min, max)),
                        EngineCode = "MKLBG",
                        VIN = "1GTHC39R9XF262667",
                        FuelType = Car.Fuel.Petrol,
                        User = user
                    }
                };

                carsToAdd.ForEach(c => context.Cars.Add(c));
                context.SaveChanges();

                #endregion

                #region Customers Seed Data
                var customersToAdd = new List<Customer> 
                {
                    new Customer
                    {
                        FirstName = "Ivan",
                        LastName = "Georgiev",
                        PhoneNumber = "0816136258",
                        User = user
                    },
                    new Customer
                    {
                        FirstName = "Georgi Ivanov",
                        LastName = "Georgiev",
                        PhoneNumber = "0875259779",
                        User = user
                    },
                    new Customer
                    {
                        FirstName = "Petar",
                        LastName = "Petrov",
                        PhoneNumber = "0886472046",
                        User = user
                    },
                    new Customer
                    {
                        CustomerId = 14,
                        FirstName = "Maria",
                        LastName = "Ilieva",
                        PhoneNumber = "0836140833",
                        User = user
                    },
                    new Customer
                    {
                        FirstName = "Haralampi",
                        LastName = "Karaivanov",
                        PhoneNumber = "0853748786",
                        User = user
                    },
                    new Customer
                    {
                        FirstName = "Hristian",
                        LastName = "Georgiev",
                        PhoneNumber = "0863933157",
                        User = user
                    }, new Customer
                    {
                        FirstName = "Ivan",
                        LastName = "Ivanov",
                        PhoneNumber = "0819116973",
                        User = user
                    },
                    new Customer
                    {
                        FirstName = "������",
                        LastName = "�������",
                        PhoneNumber = "0847900455",
                        User = user
                    }
                };
                customersToAdd.ForEach(c => context.Customers.Add(c));
                context.SaveChanges();
                #endregion

                #region Suppliers Seed Data
                var suppliersToAdd = new List<Supplier>
                {
                     new Supplier
                    {
                        Name = "Euro07",
                        DiscountPercentage = 45.0m,
                        City = "Rousse",
                        WebsiteUrl = "http://euro07.bg",
                        User = user
                    },
                    new Supplier
                    {
                        Name = "AutoPlus",
                        DiscountPercentage = 33.0m,
                        City = "Rousse",
                        WebsiteUrl = "http://autoplus.bg",
                        User = user
                    },
                    new Supplier
                    {
                        Name = "AvtoKomers",
                        DiscountPercentage = 25.0m,
                        City = "Rousse",
                        WebsiteUrl = "http://acommers.bg",
                        User = user
                    },
                    new Supplier
                    {
                        Name = "MegaParts",
                        DiscountPercentage = 10.0m,
                        City = "Sofia",
                        WebsiteUrl = "http://megaparts.bg",
                        User = user
                    },
                    new Supplier
                    {
                        Name = "Inter Cars",
                        DiscountPercentage = 40.0m,
                        City = "Rousse",
                        WebsiteUrl = "http://intercars.bg",
                        User = user
                    }
                };
                suppliersToAdd.ForEach(s => context.Suppliers.Add(s));
                context.SaveChanges();
                #endregion
            }



            //context.SaveChanges();

            //base.Seed(context);

            //if (!(context.Users.Any(u => u.UserName == "dj@dj.com")))
            //{
            //    var userStore = new UserStore<ApplicationUser>(context);
            //    var userManager = new UserManager<ApplicationUser>(userStore);
            //    var userToInsert = new ApplicationUser { UserName = "dj@dj.com", PhoneNumber = "0797697898" };
            //    userManager.Create(userToInsert, "Password@123");
            //}
        }

        public static void ClearDatabase<T>() where T : DbContext, new()
        {
            using (var context = new T())
            {
                var tableNames = context.Database.SqlQuery<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE '%Migration%'").ToList();
                foreach (var tableName in tableNames)
                {
                    foreach (var t in tableNames)
                    {
                        try
                        {
                            if (context.Database.ExecuteSqlCommand(string.Format("TRUNCATE TABLE [{0}]", tableName)) == 1)
                                break;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

                context.SaveChanges();
            }
        }
    }

}


