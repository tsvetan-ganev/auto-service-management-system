using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.ViewModels.Suppliers
{
    public class EditSupplierViewModel
    {
        public int SupplierId { get; set; }

        [Required()]
        [StringLength(maximumLength: 40, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(maximumLength: 25, MinimumLength = 3)]
        public string City { get; set; }

        [Range(0, 99)]
        [DisplayFormat(DataFormatString = "{0:0.00}%")]
        [Display(Name = "Discount")]
        public decimal DiscountPercentage { get; set; }

        [Display(Name = "Skype")]
        [StringLength(maximumLength: 32, MinimumLength = 6)]
        public string SkypeName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Website")]
        [MaxLength(48)]
        public string WebsiteUrl { get; set; }
    }
}