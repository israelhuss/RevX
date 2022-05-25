using System.Text.Json.Serialization;

namespace RevXPortal.Models
{
	public class SessionModel : ICloneable
	{
		public object this[ string propertyName ]
		{
			get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
			set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
		}
		public int Id { get; set; }
		public string UserId { get; set; }
		[Selectable(DisplayText = "Client")]
		public int ClientId { get; set; }
		[JsonConverter(typeof(DateOnlyConverter))]
		public DateOnly Date { get; set; }

		[Selectable(DisplayText = "Session Duration")]
		[JsonConverter(typeof(TimeOnlyConverter))]
		public TimeOnly StartTime { get; set; }
		[JsonConverter(typeof(TimeOnlyConverter))]
		public TimeOnly EndTime { get; set; }

		[Selectable(DisplayText = "Provider")]
		public int ProviderId { get; set; }
		public int BillingStatusId { get; set; }
		public string Notes { get; set; }
		public HourlyRate Rate { get; set; }
		public int InvoiceId { get; set; }

		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}
}
