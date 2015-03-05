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

		public decimal TotalCost 
        {
            get
            {
                decimal total = 0;
                foreach (var part in SpareParts)
                {
                    total += part.Quantity * part.Price;
                }
                return total;
            }
        }

        public decimal TotalCostWithDiscount 
        {
            get
            {
                decimal total = 0;
                foreach (var part in SpareParts)
                {
                    total += part.Quantity * part.PriceWithDiscount;
                }
                return total;
            }
        }

		public decimal Profit 
        { 
            get
            {
                return TotalCost - TotalCostWithDiscount;
            }
        }

        public List<SparePart> SpareParts { get; set; }


        public Car Car { get; set; }

        public Customer Customer { get; set; }

        public ApplicationUser User { get; set; }
    }
}
