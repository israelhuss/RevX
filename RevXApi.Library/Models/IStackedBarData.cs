namespace RevXApi.Library.Models
{
	interface IStackedBarData
	{
		public string Title { get; }
		public double BarOnePrimary { get; }
		public double BarOneSecondary { get; }
		public double BarTwoPrimary { get; }
		public double BarTwoSecondary { get; }
	}
}
