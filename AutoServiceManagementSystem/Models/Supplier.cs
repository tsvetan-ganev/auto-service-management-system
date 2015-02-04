using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoServiceManagementSystem.Models
{
	public class Supplier
	{
		public int SupplierId { get; set; }

		public string Name { get; set; }
		public string City { get; set; }
		public decimal DiscountPercentage { get; set; }
        public string WebsiteUrl { get; set; }
        public string LogoUrl { get; set; }
	}
}
