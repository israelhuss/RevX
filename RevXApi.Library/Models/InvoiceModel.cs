using System;
using System.Collections.Generic;

namespace RevXApi.Library.Models
{
	public class InvoiceModel
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public DateTime InvoiceDate { get; set; }
		public double TotalHours { get; set; }
		public List<int> SessionIds { get; set; }
	}
}
