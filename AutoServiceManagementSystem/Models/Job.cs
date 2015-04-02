using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AutoServiceManagementSystem.Models
{
    public class Job
    {
        public Job()
        {
            SpareParts = new List<SparePart>();
			DateStarted = DateTime.Now;
			Finished = false;
			Paid = false;
        }

        public int JobId { get; set; }

		[Range(1, 999999)]
        public int Mileage { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		[DataType(DataType.Date)]
		[Display(Name="Date Started")]
        public DateTime? DateStarted { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Date Finished")]
        public DateTime? DateFinished { get; set; }

        public Boolean Finished { get; set; }

        public Boolean Paid { get; set; }

        public List<SparePart> SpareParts { get; set; }

		// navigation properties
        public Car Car { get; set; }

        public Customer Customer { get; set; }

        public ApplicationUser User { get; set; }
    }
}
