using System;

namespace RevXApi.Library.Models
{
	public class SessionEmailModel
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string Student { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
		public string Notes { get; set; }
	}
}
