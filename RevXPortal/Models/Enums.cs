using DynamicExpressions;
using System.ComponentModel;

namespace RevXPortal.Models
{
	//public enum ToastLevel
	//{
	//	Info,
	//	Success,
	//	Warning,
	//	Error
	//}

	public enum ReportValueTypes
	{
		Money = 0,
		Time = 1
	}

	public enum FooterStyles
	{
		Auto = 0,
		Centered = 1,
		CenteredSlant,
		UnderLine
	}

	public enum BillingCycles
	{
		Monthly = 1,
		Biweeky = 2,
		Weekly = 3,
	}

	public enum Language
	{
		English = 0,
		Hebrew = 1
	}

	public enum ReportStyle
	{
		[Description("Bar Chart")]
		BarChart = 0,
		[Description("Line Chart")]
		LineChart = 1,
		[Description("Pie Chart")]
		PieChart = 2
	}

	public enum ReportItemView
	{
		Bar = 0,
		Stack = 1
	}

	public enum ReportItemConditionLevel
	{
		[Selectable(DisplayText = "Only If")]
		Where = 0,
		[Selectable(DisplayText = "Get")]
		Select = 1
	}

	public enum ConditionOperator
	{
		Equals = FilterOperator.Equals,
		DoesntEqual = FilterOperator.DoesntEqual,
		GreaterThan = FilterOperator.GreaterThan,
		GreaterThanOrEqual = FilterOperator.GreaterThanOrEqual,
		LessThan = FilterOperator.LessThan,
		LessThanOrEqual = FilterOperator.LessThanOrEqual,
		Contains = FilterOperator.Contains,
		NotContains = FilterOperator.NotContains,
		StartsWith = FilterOperator.StartsWith,
		EndsWith = FilterOperator.EndsWith,
		ContainsIgnoreCase = FilterOperator.ContainsIgnoreCase,
		IsEmpty = FilterOperator.IsEmpty,
		IsNotEmpty = FilterOperator.IsNotEmpty,
		ContainsKey = FilterOperator.ContainsKey,
		NotContainsKey = FilterOperator.NotContainsKey,
		ContainsValue = FilterOperator.ContainsValue,
		NotContainsValue = FilterOperator.NotContainsKey,
		Sum = 17,
		Max = 18,
		Min = 19,
	}

	public enum ReportGroupBy
	{
		Month = 0,
		Year = 1,
		Week = 2,
		Day = 3
	}
}
