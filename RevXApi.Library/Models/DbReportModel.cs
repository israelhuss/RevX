using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RevXApi.Library.Models
{
	public class DbReportModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public ReportStyle ReportStyle { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime EndDateDb { get; set; }
		public ReportGroupBy GroupBy { get; set; }
		public bool IsDefault { get; set; }
	}
}
