using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoServiceManagementSystem.Contracts;

namespace AutoServiceManagementSystem.Models.Services
{
	public class OilChange : IService
	{
        private float liters;
        private decimal price;
        private decimal pricePerLiter;

        public int OilChangeId { get; set; }
        public string Name {get;set;}
        public float Liters
        {
            get
            {
                return this.liters;
            }
            set
            {
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException(
                            "Liters added can't be less than 0.");
                    }
                    this.liters = value;
                }
            }
        }
        public decimal PricePerLiter
        {
            get { return this.pricePerLiter; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        "Price per liter can't be less than 0.");
                }
                pricePerLiter = value;
            }
        }
		public decimal Price
		{
			get { return PricePerLiter * (decimal)Liters; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        "Price can't be less than 0.");
                }
                this.price = value;
            }
		}
    }
}
