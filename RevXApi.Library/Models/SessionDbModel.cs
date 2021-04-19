using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXApi.Library.Models
{
	public class SessionDbModel
	{
		public int Id { get; set; }
		public int StudentId { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
		public int ProviderId { get; set; }
		public int BillingStatusId { get; set; }
		public string Notes { get; set; }
	}
}
