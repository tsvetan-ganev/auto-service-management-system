using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using AutoServiceManagementSystem.Models.Services;


namespace AutoServiceManagementSystem.Models
{
    /// <summary>
    /// Auto Service Management Context
    /// </summary>
    public class ASMSContext : DbContext
    {
		public ASMSContext() : base("ASMSDataBase")
		{}

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        // methods
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			/* Bulgarian plate codes are in the following format:
			 * X(X) NNNN Y(Y) 
			 * - X(X) is a district character code
			 * - NNNN is a number
			 * - Y(Y) is a series containing up to two chars
			 * Note: Rich people tend to buy letter plates with
			 * format outside the standart, e.g. X(X) NNNNNNN 
			 */
		}
    }
}
