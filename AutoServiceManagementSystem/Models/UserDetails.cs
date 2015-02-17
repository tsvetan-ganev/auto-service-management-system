using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace AutoServiceManagementSystem.Models
{
    [ComplexType()]
	public class UserDetails
	{
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Display(Name = "Company Name")]
		public string CompanyName { get; set; }

		[Display(Name = "City")]
		public string City { get; set; }
	}
}