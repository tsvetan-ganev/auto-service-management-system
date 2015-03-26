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
        public string Name { get; set; }

		[DisallowSpecialCharacters(allowDigits: true)]
        public string Code { get; set; }

        public Supplier Supplier { get; set; }

		[Range(1, 32)]
        public int Quantity { get; set; }

		[DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public Job Job { get; set; }
    }
}