using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AutoServiceManagementSystem.Models
{
    /// <summary>
    /// Customer of the autoservice.
    /// </summary>
    public class Customer
    {
        // fields
        private ICollection<Car> cars;

        // constructor
        public Customer()
        {
            this.cars = new List<Car>();
        }

        // properties
        public int CustomerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Car> Cars
        {
            get { return cars; }
            set { cars = value; }
        }
        

        // financial statistics
        public decimal MoneyOwed { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalProfit { get; set; }

        // methods
        public override string ToString()
        {
            string owner = String.Format("{0} {1}; Cars :\n", FirstName, LastName);
            StringBuilder carsSb = new StringBuilder();
            carsSb.Append("\t");
            foreach (var car in Cars)
		        carsSb.Append(car.ToString() + "\n\t ");
            return owner + carsSb.ToString();
        }
    }
}
