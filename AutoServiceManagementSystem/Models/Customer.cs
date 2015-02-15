using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using AutoServiceManagementSystem.Validation;

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
		[StringLength(maximumLength: 25, MinimumLength=3)]
		[DisallowSpecialCharacters(allowDigits: false)]
		[MaxWords(3, ErrorMessage = "There are too many words in {0}.")]
		[Display(Name="First Name")]
        public string FirstName { get; set; }

		[StringLength(maximumLength: 25, MinimumLength = 3)]
		[MaxWords(3, ErrorMessage = "There are too many words in {0}.")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		// Mobile phone numbers in BG: 
		// 08YXXXXXXX or +3598YXXXXXXX
		// TODO: Custom validation for phone numbers.
		[PhoneNumber()]
		[MinLength(10, ErrorMessage="Phone number length is exactly 10 digits.")]
		[MaxLength(10, ErrorMessage = "Phone number length is exactly 10 digits.")]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

		public ApplicationUser User { get; set; }

        public virtual ICollection<Car> Cars
        {
            get { return cars; }
            set { cars = value; }
        }
    }
}
