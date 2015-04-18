namespace AutoServiceManagementSystem.Migrations
{
    using AutoServiceManagementSystem.DAL;
    using AutoServiceManagementSystem.Helpers;
    using AutoServiceManagementSystem.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

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

            #region Seeding ApplicationUsers
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
                    UserDetails = new UserDetails
                    {
                        FirstName = "Ivan",
                        LastName = "Petrov",
                        CompanyName = "Auto Repair"
                    }
                };

                manager.Create(user, "Password@123");
            #endregion

                #region Seeding customers and their cars
                var currentUser = manager.FindByEmail("test1@test.com");
                var customersList = new List<Customer>();
                var cars = new List<Car>();

                for (int i = 0; i < 20; i++)
                {
                    var customer = CustomerSeeder.NextCustomer(currentUser);
                    for (int j = 0; j < 3; j++)
                    {
                        var car = CarSeeder.NextCar(currentUser, customer);
                        cars.Add(car);
                        customer.Cars.Add(car);
                    }
                    customersList.Add(customer);
                }

                customersList.ForEach(c => context.Customers.Add(c));
                context.SaveChanges();
                #endregion

                #region Suppliers seeding
                var suppliersToAdd = new List<Supplier>
                {
                    new Supplier
                    {
                        Name = "Euro07",
                        DiscountPercentage = 45.0m,
                        City = "Rousse",
                        WebsiteUrl = "http://euro07.bg",
                        User = currentUser
                    },
                    new Supplier
                    {
                        Name = "Auto Plus",
                        DiscountPercentage = 33.0m,
                        City = "Rousse",
                        WebsiteUrl = "http://autoplus.bg",
                        User = currentUser
                    },
                    new Supplier
                    {
                        Name = "Auto Commerce",
                        DiscountPercentage = 25.0m,
                        City = "Rousse",
                        WebsiteUrl = "http://acommers.bg",
                        User = currentUser
                    },
                    new Supplier
                    {
                        Name = "Mega Parts",
                        DiscountPercentage = 10.0m,
                        City = "Sofia",
                        WebsiteUrl = "http://megaparts.bg",
                        User = currentUser
                    },
                    new Supplier
                    {
                        Name = "Inter Cars",
                        DiscountPercentage = 40.0m,
                        City = "Rousse",
                        WebsiteUrl = "http://intercars.bg",
                        User = currentUser
                    },
					new Supplier
					{
						Name = "Auto Morgue",
						DiscountPercentage = 0.0m,
						City = "Rousse",
						WebsiteUrl = "",
						User = currentUser
					}
                };
                suppliersToAdd.ForEach(s => context.Suppliers.Add(s));
                context.SaveChanges();
                #endregion

                #region Jobs seeding
                foreach (var car in cars)
                {
                    var jobs = new List<Job>();
                    for (int i = 0; i < 3; i++)
                    {
                        jobs.Add(JobSeeder.NextJob(user, car, car.Customer));
                    }
                    jobs.ForEach(j => context.Jobs.Add(j));
                    jobs.ForEach(j => j.SpareParts.ForEach(
                        sp => context.SpareParts.Add(sp)));
                }
                SaveChanges(context);
                #endregion
            };
        }

        private static void ClearDatabase<T>() where T : DbContext, new()
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
                            Console.WriteLine(ex.Message);
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Wrapper for SaveChanges adding the Validation Messages to the generated exception
        /// </summary>
        /// <param name="context">The context.</param>
        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }
    }
}





