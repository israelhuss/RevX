using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using RevXApi.Library.Converters;

namespace RevXApi.Library.Models
{
	[KnownType(typeof(BarReportModel))]
	public class BarReportModel : IReportModel
	{
		public string UserId { get; set; }
		public int Id { get; set; }
		public string Title { get; set; }
		public ReportStyle ReportStyle { get; set; }
        [JsonConverter(typeof(DateOnlyConverter))]
		public DateOnly StartDate { get; set; }
		[JsonConverter(typeof(DateOnlyConverter))]
		public DateOnly EndDate { get; set; }
		public ReportGroupBy GroupBy { get; set; }
		public List<ReportItem> Bars { get; set; }
		public List<ReportItem> Stacks { get; set; }
		public bool IsDefault { get; set; }
	}
}
