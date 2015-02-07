using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.Validation
{
	public class DisallowSpecialCharactersAttribute : ValidationAttribute
	{
		private readonly bool allowDigits;
		private readonly char[] dissalowed = 
			{ '!', '?', '#', '^', '*', '@', '~', '.', '%', '"', '\\', '/', ']', '[', '|', '+', '\n'};

		public DisallowSpecialCharactersAttribute(bool allowDigits = true)
			: base("{0} must not contain special symbols.")
		{
			this.allowDigits = allowDigits;
			if (!allowDigits)
			{
				ErrorMessage = "{0} must not contain special symbols or numbers.";
			}
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value != null)
			{
				string valueAsString = value.ToString();
				foreach (char ch in valueAsString)
				{
					// if char isDigit() = true & allowDigits = false -> Error
					// else ignore
					if ((Char.IsSymbol(ch) || dissalowed.Contains(ch))
						|| (Char.IsDigit(ch) && !allowDigits))
					{
						var errorMessage = FormatErrorMessage(validationContext.DisplayName);
						return new ValidationResult(errorMessage);
					}
				}
			}
			return ValidationResult.Success;
		}
	}
}