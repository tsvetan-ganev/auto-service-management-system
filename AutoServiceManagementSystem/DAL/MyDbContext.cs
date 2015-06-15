using AutoServiceManagementSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.DAL
{
	public class MyDbContext : IdentityDbContext<ApplicationUser>
	{
		public MyDbContext()
			: base("ASMSDb")
		{
		}

		public static MyDbContext Create()
		{
			return new MyDbContext();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// Change the name of the table to be Users instead of AspNetUsers
			modelBuilder.Entity<IdentityUser>()
			.ToTable("AppUsers");

            modelBuilder.Entity<ApplicationUser>()
            .ToTable("AppUsers");
		}

		public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }

	}
}