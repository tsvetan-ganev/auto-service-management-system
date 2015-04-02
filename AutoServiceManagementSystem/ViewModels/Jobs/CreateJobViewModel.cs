using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AutoServiceManagementSystem.ViewModels.Jobs
{
    public class CreateJobViewModel
    {
        public CreateJobViewModel()
        {
            SpareParts = new List<EditSparePartViewModel>();
        }

        public int JobId { get; set; }

        [Range(1, 999999)]
        public int Mileage { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public List<EditSparePartViewModel> SpareParts { get; set; }
    }
}
