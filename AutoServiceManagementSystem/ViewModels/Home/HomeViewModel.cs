using AutoServiceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.ViewModels.Home
{
	public class HomeViewModel
	{
		public IEnumerable<Customer> RecentCustomers { get; set; }

		public IEnumerable<Job> RecentActiveTasks { get; set; }
	}
}