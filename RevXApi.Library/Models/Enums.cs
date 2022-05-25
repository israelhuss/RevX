using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXApi.Library.Models
{
	public enum BillingCycles
	{
		Monthly = 1,
		Biweeky = 2,
		Weekly = 3,
	}

	public enum ReportStyle
	{
		BarChart,
		LineChart,
		PieChart
	}

	public enum ReportItemView
	{
		Bar,
		Stack
	}

	public enum ReportItemConditionLevel
	{
		Where,
		Select
	}

	public enum ConditionOperator
	{
		Equals,
		DoesntEqual,
		GreaterThan,
		GreaterThanOrEqual,
		LessThan,
		LessThanOrEqual,
		Contains,
		NotContains,
		StartsWith,
		EndsWith,
		ContainsIgnoreCase,
		IsEmpty,
		IsNotEmpty,
		ContainsKey,
		NotContainsKey,
		ContainsValue,
		NotContainsValue,
		Sum,
		Max,
		Min
	}

	public enum ReportGroupBy
	{
		Month,
		Year,
		Week,
		Day
	}
}
