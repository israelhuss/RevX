namespace RevXPortal.Models
{
	public class IncomeReportModel : IStackedBarData
	{
		public string Date { get; set; }
		public double SchoolPrimary { get; set; }
		public double SchoolSecondary { get; set; }
		public double AfterSchoolPrimary { get; set; }
		public double AfterSchoolSecondary { get; set; }
		public string Title { get; set; }
		public double BarOnePrimary { get => SchoolPrimary; }
		public double BarOneSecondary { get => SchoolSecondary; }
		public double BarTwoPrimary { get => AfterSchoolPrimary; }
		public double BarTwoSecondary { get => AfterSchoolSecondary; }
	}
}
