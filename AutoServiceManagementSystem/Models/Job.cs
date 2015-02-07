using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoServiceManagementSystem.Models.Services;
using AutoServiceManagementSystem.Contracts;
using System.ComponentModel.DataAnnotations;

namespace AutoServiceManagementSystem.Models
{
    public class Job
    {
        private ICollection<SparePart> parts;

        public Job()
        {
           this.parts = new List<SparePart>();
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
                decimal result = Parts.Count > 0 ? Parts.Sum(p => p.Price) : 0;
                return result;
            }
        }

        public decimal Profit
        {
            get
            {
                decimal totalProfit = Parts.Sum(p => p.Price) 
                    - Parts.Sum(p => p.YourPrice);
                return totalProfit;
            }
        }

        public virtual ICollection<SparePart> Parts
        {
            get { return parts; }
            set { parts = value; }
        }
    }
}
