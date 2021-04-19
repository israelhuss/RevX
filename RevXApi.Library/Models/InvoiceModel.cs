using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXApi.Library.Models
{
	public class InvoiceModel
	{
		public int Id { get; set; }
		public DateTime InvoiceDate { get; set; }
		public double TotalHours { get; set; }
		public List<int> SessionIds { get; set; }
	}
}
