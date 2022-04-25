using RevXPortal.Shared.Calendar;

namespace RevXPortal.Models
{
	public class PendingSessionEventModel : ICalendarEvent
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateOnly Date { get; set; }
		public TimeOnly? StartTime { get; set; }
		public TimeOnly? EndTime { get; set; }
		public bool AllDay { get; set; } = false;
		public Type ComponentType { get; set; } = typeof(PlannedSessionEvent);
		public Language Language { get; set; } = Language.English;
		public Dictionary<string, object> Parameters { get; set; } = new();
	}
}