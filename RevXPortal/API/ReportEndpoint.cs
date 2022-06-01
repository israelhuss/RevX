using System;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using DynamicExpressions;
using Microsoft.JSInterop;
using Org.BouncyCastle.Asn1.Ocsp;
using RevXPortal.Pages;
using static System.Collections.Specialized.BitVector32;

namespace RevXPortal.API
{
	public class ReportEndpoint : IReportEndpoint
	{
		private readonly HttpClient _client;
		private readonly ISessionEndpoint _sessionEndpoint;
		private readonly IJSRuntime _jSRuntime;

		private List<SessionModel> _sessions { get; set; }

		public ReportEndpoint(HttpClient client, ISessionEndpoint sessionEndpoint, IJSRuntime jSRuntime)
		{
			_client = client;
			_sessionEndpoint = sessionEndpoint;
			_jSRuntime = jSRuntime;
		}

		public async Task<List<IncomeReportModel>> GetMonthlyIncome(DateTime startDate, DateTime endDate, string groupBy)
		{
			using (HttpResponseMessage response = await _client.GetAsync($"/api/Report/Old?StartDate={startDate}&EndDate={endDate}&groupBy={groupBy}"))
			{
				if (response.IsSuccessStatusCode)
				{
					var stuff = await response.Content.ReadAsAsync<List<IncomeReportModel>>();
					return stuff;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task<List<IReportModel>> GetAllReports()
		{
			try
			{
				List<ReportModel> stuff = await _client.GetFromJsonAsync<List<ReportModel>>("/api/Report/");
				List<IReportModel> result = new();
				foreach (ReportModel stuffItem in stuff)
				{
					if (stuffItem.ReportStyle == ReportStyle.BarChart)
					{
						ReportModel ReportModel = new()
						{
							Id = stuffItem.Id,
							StartDate = stuffItem.StartDate,
							EndDate = stuffItem.EndDate,
							ReportStyle = stuffItem.ReportStyle,
							Title = stuffItem.Title,
							GroupBy = stuffItem.GroupBy,
							Bars = stuffItem.Bars,
							Stacks = stuffItem.Stacks,
							IsDefault = stuffItem.IsDefault,
						};
						ReportModel = await FillInSessions(ReportModel);
						result.Add(ReportModel);
					}
				}
				return result;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<ReportModel> FillInSessions(ReportModel report)
		{
			if (_sessions is null)
			{
				_sessions = await _sessionEndpoint.GetAllSessions();
			}
			List<SessionModel> sessionsCopy = _sessions.Clone().ToList();
			if (report.StartDate != DateOnly.MinValue)
			{
				sessionsCopy = sessionsCopy.FindAll(s => s.Date >= report.StartDate).ToList();
			}
			if (report.EndDate != DateOnly.MaxValue)
			{
				sessionsCopy = sessionsCopy.FindAll(s => s.Date <= report.EndDate).ToList();
			}
			List<IGrouping<string, SessionModel>> groups = GroupSessions(report.GroupBy, sessionsCopy).ToList();
			report.Groups = groups;
			//if (report.Bars is not null)
			//{
			//	report.Bars.ForEach(b => { b.TotalHours = 0; b.TotalMoney = 0; }); 
			//}
			//if (report.Stacks is not null)
			//{
			//	report.Stacks.ForEach(s => { s.TotalHours = 0; s.TotalMoney = 0; });
			//}
			for (var i = 0; i < groups.Count; i++)
			{
				var group = groups[ i ];
				if (report.Bars is null || report.Bars.Count == 0)
				{
					report.Bars = new() { new() { } };
				}
				for (var j = 0; j < report.Bars.Count; j++)
				{
					var bar = report.Bars[ j ];
					var barData = ProcessData(group.ToList(), bar.ItemDetails);
					//bar.Sessions = barData;
					if (report.Stacks is null || report.Stacks.Count == 0)
					{
						report.Stacks = new() { new() { } };
					}
					for (var k = 0; k < report.Stacks.Count; k++)
					{
						var stack = report.Stacks[ k ];
						var stackData = ProcessData(barData, stack.ItemDetails);
						if (stack.Sessions is null)
						{
							stack.Sessions = new();
						}
						stack.Sessions.Add($"{group.Key}{j}", stackData);
						var hours = stackData.Sum(s => ( s.EndTime > s.StartTime ? s.EndTime - s.StartTime : TimeSpan.Zero ).TotalHours);
						var money = stackData.Sum(s => ( s.EndTime > s.StartTime ? s.EndTime - s.StartTime : TimeSpan.Zero ).TotalHours * s.Rate.Rate);
						stack.TotalMoney += money;
						stack.TotalHours += hours;
						bar.TotalHours += hours;
						bar.TotalMoney += money;
						if (report.Title == "School & After School")
						{
							await _jSRuntime.InvokeVoidAsync("log", $"Group {group.Key} Bar {j} Stack {k}: Stack Money {stack.TotalMoney} Stack Hours {stack.TotalHours}. Bar Money {bar.TotalMoney} Bar Hours {bar.TotalHours}");
						}
					}
				}
				await _jSRuntime.InvokeVoidAsync("log", "---------------------------------");
			}
			return report;
		}

		public async Task SaveReport(ReportModel report)
		{
			var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/Report")
			{
				Content = JsonContent.Create(report)
			};
			using HttpResponseMessage response = await _client.SendAsync(postRequest);
			//var data = JsonSerializer.Serialize(reportApiModel);
			//using HttpResponseMessage response = await _client.PostAsJsonAsync("/api/Report", report);
			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("Report was saved to the database.");
			}
			else
			{
				throw new Exception(response.ReasonPhrase);
			}
		}

		public async Task EditReport(ReportModel report)
		{
			var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/Report/Edit")
			{
				Content = JsonContent.Create(report)
			};
			using HttpResponseMessage response = await _client.SendAsync(postRequest);
			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("Report was updated successfully.");
			}
			else
			{
				throw new Exception(response.ReasonPhrase);
			}
		}

		public async Task DeleteReport(int id)
		{
			using HttpResponseMessage response = await _client.PostAsJsonAsync($"/api/Report/delete/{id}", new { });
			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("Report was successfully deleted.");
			}
			else
			{
				throw new Exception(response.ReasonPhrase);
			}
		}

		public async Task SetAsDefault(int id)
		{
			using HttpResponseMessage response = await _client.PostAsJsonAsync($"/api/Report/SetDefault/{id}", new { });
			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("Report was set as default.");
			}
			else
			{
				throw new Exception(response.ReasonPhrase);
			}
		}

		public record HtmlRequestContent
		{
			public string Key { get; set; }
			public string Value { get; set; }
		}

		public async Task DownloadReport()
		{
			string filename = "report";
			string rawHtml = await _jSRuntime.InvokeAsync<string>("inlineStyles", "report-base");
			HtmlRequestContent content = new() { Key = "html", Value = rawHtml };
			var req = new HttpRequestMessage(HttpMethod.Post, "/api/HtmlToPdf")
			{
				Content = JsonContent.Create(content),
			};
			req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			var response = await _client.SendAsync(req);
			if (response.IsSuccessStatusCode)
			{
				using Stream contentStream = await response.Content.ReadAsStreamAsync();
				using MemoryStream stream = new();
				await contentStream.CopyToAsync(stream);
				byte[] bytes = stream.ToArray();
				await _jSRuntime.InvokeVoidAsync("BlazorDownloadFile", $"{filename}.pdf", "application/pdf", bytes);
			}
			else
			{
				Console.WriteLine(response.ReasonPhrase);
			}
		}



		private static IEnumerable<IGrouping<string, SessionModel>> GroupSessions(ReportGroupBy groupBy, List<SessionModel> sessions)
		{
			return groupBy switch
			{
				ReportGroupBy.Month => sessions.GroupBy(s => new DateOnly(s.Date.Year, s.Date.Month, 1).ToString("MMM yyyy")),
				ReportGroupBy.Year => sessions.GroupBy(s => new DateOnly(s.Date.Year, 1, 1).ToString("yyyy")),
				ReportGroupBy.Week => sessions.GroupBy(s => s.Date.StartOfWeek(DayOfWeek.Sunday).ToString("MM/dd/yyyy")),
				ReportGroupBy.Day => sessions.GroupBy(s => s.Date.ToString("MM/dd/yyyy")),
				_ => sessions.GroupBy(s => new DateOnly(s.Date.Year, s.Date.Month, 1).ToString("MMM yyyy")),
			};
		}

		private static List<SessionModel> ProcessData(List<SessionModel> sessions, List<ReportItemDetail> conditions)
		{
			if (conditions is null)
			{
				return sessions;
			}
			var wheres = conditions.Where(c => c.Level == ReportItemConditionLevel.Where).ToList();
			var selects = conditions.Where(c => c.Level == ReportItemConditionLevel.Select).ToList();
			var filtered = DynamicFilter(sessions, wheres);
			var selected = DynamicSelect(filtered, selects);
			return selected;
		}

		private static List<SessionModel> DynamicFilter(List<SessionModel> sessions, List<ReportItemDetail> conditions)
		{
			DynamicFilterBuilder<SessionModel> predicateBuilder = new();

			if (conditions is null || conditions.Count == 0)
			{
				return sessions;
			}
			foreach (var condition in conditions)
			{
				bool isInt = int.TryParse(condition.Value.ToString(), out int pairAsInt);
				predicateBuilder.And(condition.Field, (FilterOperator)condition.Operator, ( isInt ? pairAsInt : condition.Value ));
			}
			Expression<Func<SessionModel, bool>> predicate = predicateBuilder.Build();
			var filtered = sessions.AsQueryable().Where(predicate).ToList();
			return filtered;
		}

		private static List<SessionModel> DynamicSelect(List<SessionModel> sessions, List<ReportItemDetail> conditions)
		{
			if (conditions is null || conditions.Count == 0)
			{
				return sessions;
			}
			List<SessionModel> sessionsCopy = sessions.Clone().ToList();
			foreach (var condition in conditions)
			{
				if (condition.Field == "StartTime")
				{
					DateTime parsed;
					bool isDateTime = DateTime.TryParse(condition.Value.ToString(), out parsed);
					if (!isDateTime)
					{
						throw new Exception("Start Time can only be compared to valid date");
					}
					TimeOnly key = TimeOnly.FromDateTime(parsed);
					sessionsCopy = condition.Operator switch
					{
						ConditionOperator.Max => sessionsCopy.Select(s => { s.StartTime = ( s.StartTime > key ? s.StartTime : key ); return s; }).ToList(),
						ConditionOperator.Min => sessionsCopy.Select(s => { s.StartTime = ( s.StartTime < key ? s.StartTime : key ); return s; }).ToList(),
						_ => throw new NotImplementedException(),
					};
				}
				if (condition.Field == "EndTime")
				{
					bool isDateTime = DateTime.TryParse(condition.Value.ToString(), out DateTime parsed);
					if (!isDateTime)
					{
						throw new Exception("Start Time can only be compared to valid date");
					}
					TimeOnly key = TimeOnly.FromDateTime(parsed);
					sessionsCopy = condition.Operator switch
					{
						ConditionOperator.Max => sessionsCopy.Select(s => { s.EndTime = ( s.EndTime > key ? s.EndTime : key ); return s; }).ToList(),
						ConditionOperator.Min => sessionsCopy.Select(s => { s.EndTime = ( s.EndTime < key ? s.EndTime : key ); return s; }).ToList(),
						_ => throw new NotImplementedException(),
					};
				}
			}
			return sessionsCopy;
		}
	}
}
