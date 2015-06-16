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
		//[Required]
		public string FirstName { get; set; }

		[Display(Name = "Last Name")]
        [Column("LastName")]
		//[Required]
		public string LastName { get; set; }

		[Display(Name = "Company Name")]
        [Column("CompanyName")]
		[StringLength(120, MinimumLength=3)]
		public string CompanyName { get; set; }

		[Display(Name = "City")]
        [Column("City")]
		[StringLength(120, MinimumLength = 3)]
		public string City { get; set; }
	}
}