using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoServiceManagementSystem.Validation;
using System.ComponentModel.DataAnnotations;

namespace AutoServiceManagementSystem.Models
{
    public class SparePart
    {
        public int SparePartId { get; set; }

		[Required]
        public string Name { get; set; }

		[DisallowSpecialCharacters(allowDigits: true)]
        public string Code { get; set; }

        public Supplier Supplier { get; set; }

		[Required]
		[Range(1, 32)]
        public int Quantity { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		[Range(0.1, 10000)]
        public decimal Price { get; set; }

        public Job Job { get; set; }
    }
}