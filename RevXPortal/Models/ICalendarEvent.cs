namespace RevXPortal.Models
{
	public interface ICalendarEvent
	{
		public string Title { get; set; }
		public DateOnly Date { get; set; }
		public TimeOnly? StartTime { get; set; }
		public TimeOnly? EndTime { get; set; }
		public bool AllDay { get; set; }
		public Type ComponentType { get; set; }
		public Language Language { get; set; }
		public Dictionary<string, object>? Parameters { get; set; }
	}
}
