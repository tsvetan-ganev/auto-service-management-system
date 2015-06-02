using AutoServiceManagementSystem.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.ViewModels.Customers
{
    public class EditCustomerViewModel
    {
        public int CustomerId { get; set; }

        [Required()]
        [StringLength(maximumLength: 25, MinimumLength = 3)]
        [DisallowSpecialCharacters(allowDigits: false)]
        [MaxWords(3, ErrorMessage = "There are too many words in {0}.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 25, MinimumLength = 3)]
        [MaxWords(3, ErrorMessage = "There are too many words in {0}.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [PhoneNumber()]
        [MinLength(10, ErrorMessage = "Phone number length is exactly 10 digits.")]
        [MaxLength(10, ErrorMessage = "Phone number length is exactly 10 digits.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

		[Required]
		[MaxLength(24, ErrorMessage = "City name must be no longer than 24 symbols.")]
		[MinLength(3, ErrorMessage = "City name must be no shorter than 3 symbols.")]
		public string City { get; set; }
    }
}