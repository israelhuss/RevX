using System.Text.Json.Serialization;

namespace RevXPortal.Models
{
	public class ScheduleModel
	{
		public int Id { get; set; }
		public int ClientId { get; set; }
		public int ProviderId { get; set; }

		[JsonConverter(typeof(DateOnlyConverter))]
		public DateOnly DateFrom { get; set; }

		[JsonConverter(typeof(DateOnlyConverter))]
		public DateOnly DateTo { get; set; }

		[JsonConverter(typeof(TimeOnlyConverter))]
		public TimeOnly StartTime { get; set; }

		[JsonConverter(typeof(TimeOnlyConverter))]
		public TimeOnly EndTime { get; set; }
		public List<int> DaysOfWeek { get; set; }
	}
}
