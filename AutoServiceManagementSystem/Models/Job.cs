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

        }

        public int JobId { get; set; }

		[Range(1, 999999)]
        public int CurrentMileage { get; set; }

		[DataType(DataType.Date)]
        public DateTime? DateStarted { get; set; }

		[DataType(DataType.Date)]
        public DateTime? DateFinished { get; set; }

        public Boolean Finished { get; set; }
        public Boolean Paid { get; set; }
		public decimal TotalCost { get; set; }

		public decimal Profit { get; set; }
    }
}
