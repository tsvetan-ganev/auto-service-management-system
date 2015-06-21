﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using AutoServiceManagementSystem.Models;


namespace AutoServiceManagementSystem.DAL
{
	public class CustomerRepository : ICustomerRepository, IDisposable
	{
		private MyDbContext context;

		public CustomerRepository(MyDbContext context)
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
			this.context.Configuration.LazyLoadingEnabled = true;
			return context.Customers
				.Find(customerId);
		}

		public IQueryable<Customer> Query(string userId)
		{
			return this.context.Customers.Include("User").Where(c => c.User.Id == userId);
		}

		public int GetCustomerCarsCountById(int customerId)
		{
			return context.Customers.Find(customerId).Cars.Count();
		}

		public int GetCustomerPastRepairsCount(int customerId)
		{
			int count = context.Jobs.Where(j => j.Customer.CustomerId == customerId && j.IsFinished)
				.Count();
			return count;
		}

		public int GetCustomerActiveRepairsCount(int customerId)
		{
			int count = context.Jobs.Where(j => j.Customer.CustomerId == customerId && !j.IsFinished)
				.Count();
			return count;
		}

		public decimal GetCustomerMoneyOwed(int customerId)
		{
			decimal moneyOwed = 0.0m;
			var unpaidRepairs = context.Jobs.Where(j => j.Customer.CustomerId == customerId && j.IsFinished && !j.IsPaid);
			if (unpaidRepairs.Count() > 0)
			{
				moneyOwed = (from repair in unpaidRepairs
							 select repair.SpareParts.Sum(sp => sp.Price * sp.Quantity)).Sum();
			}
			return moneyOwed;
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