using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoServiceManagementSystem.Contracts;

namespace AutoServiceManagementSystem.Models.Services
{
    public class Labour : IService
    {
        private decimal price;

        public int LabourId { get; set; }
        public string Name { get; set; }
        public decimal Price 
        {
            get { return this.price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        "Labour price can't be less than 0.");
                }
                this.price = value;
            }
        }
    }
}
