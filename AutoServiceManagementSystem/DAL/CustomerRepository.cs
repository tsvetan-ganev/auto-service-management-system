using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using AutoServiceManagementSystem.Models;


namespace AutoServiceManagementSystem.DAL
{
	public class CustomerRepository : ICustomerRepository, IDisposable
	{
		private ASMSContext context;

		public CustomerRepository(ASMSContext context)
		{
			this.context = context;
		}

		#region ICustomerRepository Implementation

		public IEnumerable<Customer> GetCustomers()
		{
			return context.Customers.ToList();
		}

		public Customer GetCustomerById(int? customerId)
		{
			return context.Customers.Find(customerId);
		}

		public void InsertCustomer(Customer customer)
		{
			context.Customers.Add(customer);
		}

		public void DeleteCustomer(int customerId)
		{
			Customer customer = context.Customers.Find(customerId);
			context.Customers.Remove(customer);
		}

		public void UpdateCustomer(Customer customer)
		{
			context.Entry(customer).State = EntityState.Modified;
		}

		public void Save()
		{
			context.SaveChanges();
		}

		#endregion

		private bool disposed = false;

		public virtual void Dispose(bool disposing)
		{
			if (!disposed && disposing)
			{
				context.Dispose();
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}