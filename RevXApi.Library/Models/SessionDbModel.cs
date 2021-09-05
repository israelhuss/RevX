using System;

namespace RevXApi.Library.Models
{
	public class SessionDbModel
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public int StudentId { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
		public int ProviderId { get; set; }
		public int BillingStatusId { get; set; }
		public string Notes { get; set; }
	}
}
