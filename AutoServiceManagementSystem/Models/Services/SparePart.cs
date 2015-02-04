using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using AutoServiceManagementSystem.Contracts;

namespace AutoServiceManagementSystem.Models.Services
{
    public class SparePart : IService
    {
        private decimal customerPrice;
        private int quantity;

        public SparePart()
        {
            Quantity = 1;
        }

        [Key]
        public int PartId { get; set; }

		public string Name { get; set; }
		public string DealerCode { get; set; }
		public Supplier Supplier { get; set; }
        public int Quantity
        {
            get { return this.quantity; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        "Quantity can't be set to less than zero.");
                }
                this.quantity = value;
            }
        }
		public decimal Price 
        {
            get { return customerPrice * Quantity; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        "Item's price can't be less than 0.");
                }
                customerPrice = value;
            }
        }
		public decimal YourPrice 
		{
			get 
            {
                return Quantity * Price - Quantity * 
                    (Price * Supplier.DiscountPercentage / 100.0m);
            }
		}
    }
}
