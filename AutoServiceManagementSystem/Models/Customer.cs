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

		[Required()]
		[StringLength(maximumLength: 15, MinimumLength=3)]
		[Display(Name="First Name")]
        public string FirstName { get; set; }

		[StringLength(maximumLength: 15, MinimumLength = 3)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		// Mobile phone numbers in BG: 
		// 08YXXXXXXX or +3598YXXXXXXX
		// TODO: Custom validation for phone numbers.
		[StringLength(maximumLength: 13, MinimumLength = 10)]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

        public virtual ICollection<Car> Cars
        {
            get { return cars; }
            set { cars = value; }
        }
    }
}
