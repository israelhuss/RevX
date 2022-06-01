using System.Text.Json.Serialization;

namespace RevXPortal.Models
{
	public class ReportModel : IReportModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public ReportStyle ReportStyle { get; set; }
		[JsonConverter(typeof(DateOnlyConverter))]
		public DateOnly StartDate { get; set; }
		[JsonConverter(typeof(DateOnlyConverter))]
		public DateOnly EndDate { get; set; }
		public ReportGroupBy GroupBy { get; set; }
		public bool IsDefault { get; set; }

		public IEnumerable<IGrouping<string, SessionModel>> Groups { get; set; }

		public List<ReportItem> Bars { get; set; }
		public List<ReportItem> Stacks { get; set; }
	}
}
