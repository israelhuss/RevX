using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RevXPortal.FormValidationAttributes
{
	public class NotAfterToday : ValidationAttribute
	{

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if ((DateTime)value <= DateTime.Now )
			{
				return ValidationResult.Success;
			}
			return new ValidationResult(ErrorMessage ?? "Make sure your date is not after today.");
		}
	}
}
