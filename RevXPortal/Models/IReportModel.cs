using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXPortal.Models
{
	public interface IReportModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public ReportStyle ReportStyle { get; set; }
		public DateOnly StartDate { get; set; }
		public DateOnly EndDate { get; set; }
		public ReportGroupBy GroupBy { get; set; }
		public bool IsDefault { get; set; }
	}
}
