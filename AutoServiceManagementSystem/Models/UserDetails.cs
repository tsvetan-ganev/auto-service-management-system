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
        [Column("FirstName")]
		public string FirstName { get; set; }

		[Display(Name = "Last Name")]
        [Column("LastName")]
		public string LastName { get; set; }

		[Display(Name = "Company Name")]
        [Column("CompanyName")]
		public string CompanyName { get; set; }

		[Display(Name = "City")]
        [Column("City")]
		public string City { get; set; }
	}
}