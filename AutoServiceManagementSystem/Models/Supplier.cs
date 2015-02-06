﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AutoServiceManagementSystem.Models
{
	public class Supplier
	{
		public int SupplierId { get; set; }

		[Required()]
		public string Name { get; set; }

		public string City { get; set; }

		[Range(0, 99)]
		[DisplayFormat(DataFormatString = "{0:0.00}%")]
		[Display(Name="Discount")]
		public decimal DiscountPercentage { get; set; }

		[DataType(DataType.Url)]
		[Display(Name="Website")]
        public string WebsiteUrl { get; set; }

		[DataType(DataType.Url)]
		[Display(Name = "Logo Url")]
        public string LogoUrl { get; set; }
	}
}
