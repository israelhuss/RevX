using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
			string[] startTimeSplit = model.StartTime.ToString().Split(':');
			TimeSpan startTimeAsTimeSpan = new TimeSpan(int.Parse(startTimeSplit[0]), int.Parse(startTimeSplit[1]), 0);

			string[] endTimeSplit = model.EndTime.ToString().Split(':');
			TimeSpan endTimeAsTimeSpan = new TimeSpan(int.Parse(endTimeSplit[0]), int.Parse(endTimeSplit[1]), 0);

			if ((endTimeAsTimeSpan - startTimeAsTimeSpan).TotalHours > _duration)
			{
				return new ValidationResult(ErrorMessage ?? $"Session can't be longer than {_duration} hours.");
			}
			return ValidationResult.Success;
		}
	}
}
