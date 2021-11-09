namespace RevXPortal.Models
{
	public class IncomeReportModel : IStackedBarData
	{
		public string Date { get; set; }
		public ReportSegmant SchoolPrimary { get; set; }
		public ReportSegmant SchoolSecondary { get; set; }
		public ReportSegmant AfterSchoolPrimary { get; set; }
		public ReportSegmant AfterSchoolSecondary { get; set; }
		public string Title { get; set; }
		public string TooltipText { get; set; }
		public double BarOnePrimary { get => SchoolPrimary.Hours; }
		public double BarOneSecondary { get => SchoolSecondary.Hours; }
		public double BarTwoPrimary { get => AfterSchoolPrimary.Hours; }
		public double BarTwoSecondary { get => AfterSchoolSecondary.Hours; }
	}
}
