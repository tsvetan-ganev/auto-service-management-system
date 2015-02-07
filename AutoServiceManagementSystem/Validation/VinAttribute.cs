using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.Validation
{
	public class VinAttribute : ValidationAttribute
	{
		public VinAttribute()
			: base("Invalid VIN code.")
		{

		}

		/// <summary>
		/// Checks if a VIN code is the correct length (17) 
		/// and if it contains only valid symbols (latin alpha-numeric).
		/// </summary>
		/// <param name="value">Vehicle identification number</param>
		/// <param name="validationContext">Describes the context in which the validation check is done.</param>
		/// <returns>Success or ErrorMessage</returns>
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value != null)
			{
				string vin = value.ToString().ToUpper();
				if (vin.Length != 17)
				{
					var errorMessage = FormatErrorMessage(validationContext.DisplayName);
					return new ValidationResult(errorMessage + " VIN is exactly 17 characters long.");
				}

				foreach (var ch in vin)
				{
					if (!((ch <= 'Z' && ch >= 'A') 
						|| Char.IsDigit(ch)))
					{
						var errorMessage = FormatErrorMessage(validationContext.DisplayName);
						return new ValidationResult(errorMessage + " VIN must contain only latin alhpa-numeric characters.");
					}
				}
			}
			return ValidationResult.Success;
		}
	}
}