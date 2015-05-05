using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.ViewModels.Jobs
{
	public class DisplaySparePartViewModel
	{
		public string Name { get; set; }

		public string Code { get; set; }

		public string SupplierName { get; set; }

		public decimal Discount { get; set; }

		public int Quantity { get; set; }

		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		[DataType(DataType.Currency)]
		public decimal PriceAfterDiscount
		{
			get
			{
				return Price - (Price * Discount / 100);
			}
		}

		[DataType(DataType.Currency)]
		public decimal SubTotal
		{
			get
			{
				return Price * Quantity;
			}
		}

		[DataType(DataType.Currency)]
		public decimal SubTotalAfterDiscount
		{
			get
			{
				return PriceAfterDiscount * Quantity;
			}
		}
	}
}