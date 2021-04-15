using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevXPortal.Models
{
	public class InvoiceModel
	{
		public int Id { get; set; }
		public DateTime InvoiceDate { get; set; } = DateTime.Now;
		public List<int> SessionIds { get; set; }
		public double TotalHours { get; set; }
	}
}
