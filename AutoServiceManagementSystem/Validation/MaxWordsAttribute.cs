using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.Validation
{
	public class MaxWordsAttribute : ValidationAttribute
	{
		private readonly int maxWords;

		public MaxWordsAttribute(int maxWords)
			:base("{0} has too many words.")
		{
			this.maxWords = maxWords;
		}

		protected override ValidationResult IsValid(object value, ValidationContext context)
		{
			if (value != null)
			{
				var valueAsString = value.ToString();
				if (valueAsString.Split(' ').Length > maxWords)
				{
					var errorMessage = FormatErrorMessage(context.DisplayName);
					return new ValidationResult(errorMessage);
				}
			}
			return ValidationResult.Success;
		}
	}
}