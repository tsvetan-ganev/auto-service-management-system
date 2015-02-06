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
        private OilChange oilChange;
        private Labour labour;

        public Job()
        {
           this.parts = new List<SparePart>();
           this.oilChange = new OilChange();
           this.labour = new Labour();
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
                result += OilChange.Price;
                result += Labour.Price;

                return result;
            }
        }

        public decimal Profit
        {
            get
            {
                decimal totalProfit = Parts.Sum(p => p.Price) 
                    - Parts.Sum(p => p.YourPrice);
                totalProfit += OilChange.Price;
                totalProfit += Labour.Price;
                return totalProfit;
            }
        }

        public virtual ICollection<SparePart> Parts
        {
            get { return parts; }
            set { parts = value; }
        }

        public virtual OilChange OilChange
        {
            get { return oilChange; }
            set { oilChange = value; }
        }

		public int LabourId { get; set; }
        public virtual Labour Labour
        {
            get { return labour; }
            set { labour = value; }
        }
    }
}
