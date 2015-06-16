using System;
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
		[StringLength(maximumLength: 40, MinimumLength = 3)]
		public string Name { get; set; }

		[StringLength(maximumLength: 25, MinimumLength = 3)]
		public string City { get; set; }

		[Range(0, 99)]
		[DisplayFormat(DataFormatString = "{0:0.00}%")]
		[Display(Name="Discount")]
		public decimal DiscountPercentage { get; set; }

        [Display(Name = "Skype")]
		[StringLength(120, MinimumLength = 6)]
        public string SkypeName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

		[DataType(DataType.Url)]
        [UIHint("UrlNewWindow")]
		[Display(Name="Website")]
        [MaxLength(48)]
        public string WebsiteUrl { get; set; }

        public bool IsDeleted { get; set; }

		public bool IsDefault { get; set; }

		public ApplicationUser User { get; set; }
	}
}
