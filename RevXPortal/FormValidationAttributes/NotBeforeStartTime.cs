using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RevXPortal.FormValidationAttributes
{
	public class NotBeforeStartTime : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var model = (ManageSessionModel)value;
			if (model.StartTime >= new TimeSpan() && model.EndTime >= new TimeSpan() && model.StartTime < model.EndTime)
			{
				return ValidationResult.Success;
			}
			else
			{
				return new ValidationResult(ErrorMessage ?? "End Time cannot be before Start Time.");
			}
		}
	}
}
