using System;

namespace RevXApi.Library.Models
{
	public class HourlyRate
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public double Rate { get; set; }
		public int? ProviderId { get; set; }
	}
}
