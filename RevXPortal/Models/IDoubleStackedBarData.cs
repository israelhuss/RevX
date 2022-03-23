namespace RevXPortal.Models
{
	public interface IDoubleStackedBarData
	{
		public double StackOnePrimary { get; set; }
		public double StackOneSecondery { get; set; }
		public double StackTwoPrimary { get; set; }
		public double StackTwoSecondery { get; set; }

		public string StackOnePrimaryText { get; set; }
		public string StackOneSeconderyText { get; set; }
		public string StackTwoPrimaryText { get; set; }
		public string StackTwoSeconderyText { get; set; }
	}
}
