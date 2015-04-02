using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using System.Web.Mvc;namespace AutoServiceManagementSystem.ViewModels.Jobs
{
    public class UserSuppliersViewModel
    {
        // for the dropdown list
        [Display(Name = "Supplier")]
        public int SelectedSupplierId { get; set; }
        public IEnumerable<SelectListItem> UserSuppliers { get; set; }
    }
}