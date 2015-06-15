using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.ViewModels.Home
{
	public class DebtorViewModel
	{
		public int CustomerId { get; set; }

		public string Name { get; set; }

		[DataType(DataType.Currency)]
		public decimal MoneyOwed { get; set; }
	}
}