using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RevXPortal.FormValidationAttributes
{
	public class NotNegative : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			try
			{
				var valueAsTimeSpan = (TimeSpan)value;
				if (valueAsTimeSpan >= new TimeSpan())
				{
					return ValidationResult.Success;
				}
				else
				{
					return new ValidationResult(ErrorMessage ?? $"The {validationContext.DisplayName} field is required.");
				}
			}
			catch (Exception)
			{
				return new ValidationResult(ErrorMessage ?? $"The {validationContext.DisplayName} field is required.");
			}
			
		}
	}
}
