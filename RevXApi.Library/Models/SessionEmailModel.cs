using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXApi.Library.Models
{
	public class SessionEmailModel
	{
		public int Id { get; set; }
		public string Student { get; set; }
		public DateTime Date { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public string Notes { get; set; }
	}
}
