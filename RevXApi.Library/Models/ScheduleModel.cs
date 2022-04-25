using System;
using System.Collections.Generic;

namespace RevXApi.Library.Models
{
	public class ScheduleModel
	{
		public int Id { get; set; }
		public int ClientId { get; set; }
		public int ProviderId { get; set; }
		public DateOnly DateFrom { get; set; }
		public DateOnly DateTo { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
		public List<int> DaysOfWeek { get; set; }
	}
}
