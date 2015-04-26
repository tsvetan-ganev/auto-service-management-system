using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoServiceManagementSystem.Validation;
using System.ComponentModel.DataAnnotations;

namespace AutoServiceManagementSystem.ViewModels.Jobs
{
    public class EditSparePartViewModel
    {
		public EditSparePartViewModel()
		{
            Suppliers = new UserSuppliersViewModel();
			Quantity = 1;
		}

        public int SparePartId { get; set; }

		[Required]
        public string Name { get; set; }

        [DisallowSpecialCharacters(allowDigits: true)]
        public string Code { get; set; }

		[Required]
        [Range(1, 32)]
        public int Quantity { get; set; }

		[Required]
        [DataType(DataType.Currency)]
		[Range(0, 10000)]
        public decimal Price { get; set; }

        public UserSuppliersViewModel Suppliers { get; set; }
    }
}