using System;
using System.Net.Http.Json;
using System.Text.Json;

namespace RevXPortal.API
{
	public class ReportEndpoint : IReportEndpoint
	{
		private readonly HttpClient _client;

		public ReportEndpoint(HttpClient client)
		{
			_client = client;
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
				List<ReportApiModel> stuff = await _client.GetFromJsonAsync<List<ReportApiModel>>("/api/Report/");
				List<IReportModel> result = new();
				foreach (ReportApiModel stuffItem in stuff)
				{
					if (stuffItem.ReportStyle == ReportStyle.BarChart)
					{
						BarReportModel barReportModel = new()
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
						result.Add(barReportModel);
					}
				}
				return result;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task SaveReport(BarReportModel report)
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

		public async Task EditReport(BarReportModel report)
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
	}
}
