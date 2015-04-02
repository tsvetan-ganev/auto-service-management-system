using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.ViewModels.Jobs
{
    public class EditJobViewModel
    {
        [Range(1, 999999)]
        public int Mileage { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public Boolean Finished { get; set; }

        public Boolean Paid { get; set; }

        public List<EditSparePartViewModel> SpareParts { get; set; }
    }
}