using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using AutoServiceManagementSystem.Models;


namespace AutoServiceManagementSystem.DAL
{
    /// <summary>
    /// Auto Service Management Context
    /// </summary>
    public class ASMSContext : DbContext
    {
		public ASMSContext()
			: base("name=ASMSDb")
		{}

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        // methods
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			
		}
    }
}
