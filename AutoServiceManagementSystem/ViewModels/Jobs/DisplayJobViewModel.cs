using AutoServiceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.ViewModels.Jobs
{
	public class DisplayJobViewModel
	{
		public int JobId { get; set; }

		public int Mileage { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Date Started")]
		public DateTime? DateStarted { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Last Modified")]
		public DateTime? LastModified { get; set; }

		[Display(Name = "Finished")]
		[UIHint("IsFinished")]
		public Boolean IsFinished { get; set; }

		[Display(Name = "Paid")]
		[UIHint("IsPaid")]
		public Boolean IsPaid { get; set; }

		public List<DisplaySparePartViewModel> SpareParts { get; set; }

		[DataType(DataType.Currency)]
		public decimal Total
		{
			get
			{
				return SpareParts.Sum(sp => sp.SubTotal);
			}
		}

		[DataType(DataType.Currency)]
		public decimal TotalAfterDiscounts
		{
			get
			{
				return SpareParts.Sum(sp => sp.SubTotalAfterDiscount);
			}
		}
	}
}