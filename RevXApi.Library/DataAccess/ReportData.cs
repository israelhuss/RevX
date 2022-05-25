using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace RevXApi.Library.DataAccess
{
	public class ReportData : IReportData
	{
		private readonly ISqlDataAccess _sql;

		public ReportData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public List<IncomeReportModel> GetMonthlyIncome(DateTime startDate, DateTime endDate, string userId, string groupBy)
		{
			var stuff = _sql.LoadData<IncomeReportModel, dynamic>("dbo.spReport_SchoolAndAfterSchool", new { startDate, endDate, userId, groupBy }, "RevXData");
			return stuff;
		}

		public List<IReportModel> GetReports(string userId)
		{
			List<IReportModel> output = new();
			List<DbReportModel> reports = _sql.LoadData<DbReportModel, dynamic>("dbo.spReport_GetAll", new { userId }, "RevXData");

			foreach (var report in reports)
			{
				if (report.ReportStyle == ReportStyle.BarChart)
				{
					BarReportModel barReportModel = new()
					{
						Id = report.Id,
						Title = report.Title,
						StartDate = DateOnly.FromDateTime(report.StartDate),
						EndDate = DateOnly.FromDateTime(report.EndDate),
						GroupBy = report.GroupBy,
						ReportStyle = report.ReportStyle,
						IsDefault = report.IsDefault,
					};
					List<ReportItem> reportItems = _sql.LoadData<ReportItem, dynamic>("dbo.spReportItem_GetByReportId", new { ReportId = barReportModel.Id }, "RevXData");
					barReportModel.Bars = new();
					barReportModel.Stacks = new();
					foreach (ReportItem reportItem in reportItems)
					{
						if (reportItem.ViewAs == ReportItemView.Bar)
						{
							barReportModel.Bars.Add(reportItem);
						}
						else if (reportItem.ViewAs == ReportItemView.Stack)
						{
							barReportModel.Stacks.Add(reportItem);
						}

						List<ReportItemDetail> reportItemDetails = _sql.LoadData<ReportItemDetail, dynamic>("dbo.spReportItemDetail_GetByReportItemId", new { ReportItemId = reportItem.Id }, "RevXData");
						reportItem.ItemDetails = reportItemDetails;
						//foreach (ReportItemDetail reportItemDetail in reportItemDetails)
						//{
						//	reportItem.ItemDetails.Add(reportItemDetail);
						//	List<KeyValuePair<object, ConditionOperator>> conditions = _sql.LoadData<KeyValuePair<object, ConditionOperator>, dynamic>("dbo.spReportItemDetailCondition_GetByReportItemDetailId", new { ReportItemDetailId = reportItemDetail.Id }, "RevXData");
						//	reportItemDetail.Conditions = conditions;
						//}
					}
					output.Add(barReportModel);
				}
			}
			return output;
		}

		public record ReportItemDetailDb
		{
			public int ReportItemId { get; set; }
			public string Field { get; set; }
			public object Value { get; set; }
			public ConditionOperator Operator { get; set; }
			public ReportItemConditionLevel Level { get; set; }
		}

		private int InsertReport(BarReportModel report)
		{
			int reportId = _sql.SaveDataInTransactionWithResult<int, dynamic>("dbo.spReport_Insert", new { report.Id, report.UserId, report.Title, report.ReportStyle, StartDate = report.StartDate.ToShortDateString(), EndDate = report.EndDate.ToShortDateString(), report.GroupBy, report.IsDefault })[ 0 ];
			if (reportId == 0)
			{
				throw new Exception();
				// return 0;
			}
			report.Id = reportId;
			if (report.Bars is not null)
			{
				foreach (var bar in report.Bars)
				{
					var barId = _sql.SaveDataInTransactionWithResult<int, dynamic>("dbo.spReportItem_Insert", new { ReportId = report.Id, bar.ViewAs, bar.Color, bar.Nickname })[ 0 ];
					bar.Id = barId;
					if (bar.Id == 0)
					{
						throw new Exception();
						// return 0;
					}
					if (bar.ItemDetails is not null)
					{
						foreach (var detail in bar.ItemDetails)
						{
							detail.Id = _sql.SaveDataInTransactionWithResult<int, dynamic>("dbo.spReportItemDetail_Insert", new { ReportItemId = bar.Id, detail.Field, Value = detail.Value.ToString(), detail.Operator, detail.Level })[ 0 ];
							if (detail.Id == 0)
							{
								throw new Exception();
								//return 0;
							}
						}
					}
				}
			}
			if (report.Stacks is not null)
			{
				foreach (var stack in report.Stacks)
				{
					stack.Id = _sql.SaveDataInTransactionWithResult<int, dynamic>("dbo.spReportItem_Insert", new { ReportId = report.Id, stack.ViewAs, stack.Color, stack.Nickname })[ 0 ];
					if (stack.Id == 0)
					{
						throw new Exception();
						//return 0;
					}
					if (stack.ItemDetails is not null)
					{
						foreach (var detail in stack.ItemDetails)
						{
							//ReportItemDetailDb reportItemDetailDb = new()
							//{
							//	ReportItemId = stack.Id
							//	Field = detail.Field,
							//}
							detail.Id = _sql.SaveDataInTransactionWithResult<int, dynamic>("dbo.spReportItemDetail_Insert", new { ReportItemId = stack.Id, detail.Field, Value = detail.Value.ToString(), detail.Operator, detail.Level })[ 0 ];
							if (detail.Id == 0)
							{
								throw new Exception();
								//return 0;
							}
						}
					}
				}
			}
			return reportId;
		}

		public int SaveReport(BarReportModel report)
		{
			_sql.StartTransaction("RevXData");
			try
			{
				return InsertReport(report);
			}
			catch (Exception)
			{
				_sql.RollBackTransaction();
				return -1;
			}
		}

		public void SetAsDefault(int id, string userId)
		{
			_sql.SaveData("dbo.spReport_SetAsDefault", new { Id = id, userId }, "RevXData");
		}

		public void Delete(int id, string userId)
		{
			_sql.SaveData("dbo.spReport_Delete", new { Id = id, userId }, "RevXData");
		}

		public void DeleteInTranaction(int id, string userId)
		{
			_sql.SaveDataInTransaction<dynamic>("dbo.spReport_Delete", new { Id = id, userId });
		}

		public int EditReport(BarReportModel report)
		{
			_sql.StartTransaction("RevXData");
			try
			{
				DeleteInTranaction(report.Id, report.UserId);
				return InsertReport(report);
			}
			catch (Exception ex)
			{
				_sql.RollBackTransaction();
				//return -1;
				throw;
			}
		}
	}
}
