using System;
using System.Collections.Generic;
using System.Linq;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.DAL
{
	public interface ICustomerRepository : IDisposable
	{
		IEnumerable<Customer> GetCustomers();
		Customer GetCustomerById(int? customerId);
		int GetCustomerCarsCountById(int customerId);
		int GetCustomerPastRepairsCount(int customerId);
		int GetCustomerActiveRepairsCount(int customerId);
		decimal GetCustomerMoneyOwed(int customerId);
		void InsertCustomer(Customer customer);
		void DeleteCustomer(int customerId);
		void UpdateCustomer(Customer customer);
		void Save();
	}
}