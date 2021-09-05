using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public class ReportEndpoint : IReportEndpoint
	{
		private readonly HttpClient _client;

		public ReportEndpoint(HttpClient client)
		{
			_client = client;
		}

		public async Task<List<IncomeReportModel>> GetMonthlyIncome(DateTime startDate, DateTime endDate, string userId, string groupBy)
		{
			using (HttpResponseMessage response = await _client.GetAsync($"/api/Report?StartDate={startDate}&EndDate={endDate}&userId={userId}&groupBy={groupBy}"))
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
	}
}
