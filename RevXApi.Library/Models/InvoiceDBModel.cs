using System;

namespace RevXApi.Library.Models
{
	public class InvoiceDBModel
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public DateTime InvoiceDate { get; set; }
		public double TotalHours { get; set; }
	}
}
