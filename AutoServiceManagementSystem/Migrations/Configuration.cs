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
	using System.Linq;

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
                    UserDetails = new UserDetails
                    {
                        FirstName = "Ivan",
                        LastName = "Petrov",
                        CompanyName = "Auto Repair"
                    }
                };

                manager.Create(user, "Password@123");

                #region Seeding customers and their cars
				var currentUser = manager.FindByEmail("test1@test.com");
				var customersList = new List<Customer>();

				for (int i = 0; i < 10; i++)
				{
					var customer = CustomerSeeder.NextCustomer(currentUser);
					for (int j = 0; j < 3; j++)
					{
						customer.Cars.Add(CarSeeder.NextCar(currentUser, customer));
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
                        Name = "AutoPlus",
                        DiscountPercentage = 33.0m,
                        City = "Rousse",
                        WebsiteUrl = "http://autoplus.bg",
                        User = currentUser
                    },
                    new Supplier
                    {
                        Name = "AvtoKomers",
                        DiscountPercentage = 25.0m,
                        City = "Rousse",
                        WebsiteUrl = "http://acommers.bg",
                        User = currentUser
                    },
                    new Supplier
                    {
                        Name = "MegaParts",
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
                    }
                };
                suppliersToAdd.ForEach(s => context.Suppliers.Add(s));
                context.SaveChanges();
                #endregion
            };
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





