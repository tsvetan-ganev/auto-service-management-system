using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.Models
{
    public class SparePart
    {
        public int SparePartId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Supplier Supplier { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceWithDiscount
        {
            get
            {
                return Price - (Price * Supplier.DiscountPercentage / 100);
            }
        }
        public Job Job { get; set; }
    }
}