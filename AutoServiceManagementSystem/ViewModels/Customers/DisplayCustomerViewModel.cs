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

		[DataType(DataType.Date)]
		public DateTime DateAdded { get; set; }

		public int CarsCount { get; set; }
	}
}