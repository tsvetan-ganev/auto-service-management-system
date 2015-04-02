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
        //public int SparePartId { get; set; }

        public string Name { get; set; }

        [DisallowSpecialCharacters(allowDigits: true)]
        public string Code { get; set; }

        [Range(1, 32)]
        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public UserSuppliersViewModel Suppliers { get; set; }
    }
}