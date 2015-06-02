using AutoServiceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.ViewModels.Customers
{
	public class DisplayCustomerViewModel
	{
		public int Id { get; set; }

		[Display(Name="First Name")]
        public string FirstName { get; set; }

		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

		public string City { get; set; }

		[DataType(DataType.Date)]
		public DateTime DateAdded { get; set; }

		[Display(Name = "Cars")]
		public int CarsCount { get; set; }

		[Display(Name = "Past Repairs")]
		public int PastRepairsCount { get; set; }

		[Display(Name = "Active Repairs")]
		public int ActiveRepairsCount { get; set; }

		[Display(Name = "Money Owed")]
		[DataType(DataType.Currency)]
		public decimal MoneyOwed { get; set; }
	}
}