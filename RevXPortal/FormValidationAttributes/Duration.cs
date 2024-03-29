﻿using RevXPortal.Models;
using System.ComponentModel.DataAnnotations;

namespace RevXPortal.FormValidationAttributes
{
	public class Duration : ValidationAttribute
	{
		private readonly int _duration;

		public Duration(int duration)
		{
			_duration = duration;
		}
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var model = (ManageSessionModel)value;

			if ((model.EndTime - model.StartTime).TotalHours > _duration)
			{
				return new ValidationResult(ErrorMessage ?? $"Session can't be longer than {_duration} hours.");
			}
			return ValidationResult.Success;
		}
	}
}
