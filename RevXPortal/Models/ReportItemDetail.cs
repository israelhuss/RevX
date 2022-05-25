namespace RevXPortal.Models
{
	public class ReportItemDetail
	{
		public int Id { get; set; }
		public string Field { get; set; }
		public object Value { get; set; }
		public ConditionOperator Operator { get; set; }
		public ReportItemConditionLevel Level { get; set; }
	}
}
