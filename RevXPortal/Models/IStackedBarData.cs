namespace RevXPortal.Models
{
	public interface IStackedBarData
	{
		public string Title { get; }
		public string TooltipText { get; set; }
		public double BarOnePrimary { get; }
		public double BarOneSecondary { get; }
		public double BarTwoPrimary { get; }
		public double BarTwoSecondary { get; }
	}
}
