using System;
using System.Globalization;

namespace RevXPortal.Converters
{
	public static class TimeConverters
	{
		public static TimeSpan Convert24HourStringToTimeSpan(string timeString)
		{
			var split = timeString.Split(':');
			if (int.TryParse(split[0], out int hours) && int.TryParse(split[1], out int minutes))
			{
				var output = new TimeSpan(hours, minutes, 00);
				return output;
			}
			else
			{
				throw new FormatException();
			}
		}


		public static string ConvertTimeSpanTo12HourString(TimeSpan time)
		{
			var newTime = TimeOnly.FromTimeSpan(time);
			return newTime.ToString("h:mm tt", CultureInfo.InvariantCulture);
			string output = String.Empty;

			int hours = time.Hours;
			int minutes = time.Minutes;
			if (hours == 0)
			{
				output = $"12:{minutes:D2} AM";
			}
			else if (hours < 12)
			{
				output = $"{hours}:{minutes:D2} AM";
			}
			else if (hours == 12)
			{
				output = $"{hours}:{minutes:D2} PM";
			}
			else if (hours > 12)
			{
				output = $"{hours - 12}:{minutes:D2} PM";
			}
			return output;
		}

		public static TimeSpan Convert12HourStringToTimeSpan(string timeString)
		{
			var output = new TimeSpan();
			var split = timeString.Split(' ');
			var prefix = split[1];
			var hoursNminutes = split[0].Split(':');
			if (int.TryParse(hoursNminutes[0], out int hours) && int.TryParse(hoursNminutes[1], out int minutes))
			{
				if (prefix.ToLower() == "am")
				{
					if (hours == 12)
					{
						output = new TimeSpan(00, minutes, 00);
					}
					else
					{
						output = new TimeSpan(hours, minutes, 00);
					}
				}
				else if (prefix.ToLower() == "pm")
				{
					if (hours == 12)
					{
						output = new TimeSpan(12, minutes, 00);
					}
					else
					{
						output = new TimeSpan(hours + 12, minutes, 00);
					}
				}

				return output;
			}
			else
			{
				throw new FormatException();
			}
		}
	}
}
