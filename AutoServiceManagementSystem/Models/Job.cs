using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoServiceManagementSystem.Models.Services;
using AutoServiceManagementSystem.Contracts;

namespace AutoServiceManagementSystem.Models
{
    public class Job
    {
        private ICollection<SparePart> parts;
        private Diag diagnosis;
        private OilChange oilChange;
        private Labour labour;

        public Job()
        {
            parts = new List<SparePart>();
            diagnosis = new Diag();
            oilChange = new OilChange();
            labour = new Labour();
        }

        public int JobId { get; set; }

        public int CurrentMileage { get; set; }
        public DateTime? DateStarted { get; set; }
        public DateTime? DateFinished { get; set; }
        public Boolean Finished { get; set; }
        public Boolean Paid { get; set; }
        public decimal TotalCost
        {
            get
            {
                decimal result = Parts.Count > 0 ? Parts.Sum(p => p.Price) : 0;
                result += Diagnosis.Price;
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
                totalProfit += Diagnosis.Price;
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
        public virtual Diag Diagnosis
        {
            get { return diagnosis; }
            set { diagnosis = value; }
        }
        public virtual OilChange OilChange
        {
            get { return oilChange; }
            set { oilChange = value; }
        }
        public virtual Labour Labour
        {
            get { return labour; }
            set { labour = value; }
        }
    }
}
