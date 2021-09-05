using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public class HourlyRateEndpoint : IHourlyRateEndpoint
	{
		private readonly HttpClient _client;

		public HourlyRateEndpoint(HttpClient client)
		{
			_client = client;
		}

		public async Task<List<HourlyRate>> GetAll(string userId)
		{
			using (HttpResponseMessage response = await _client.GetAsync($"/api/HourlyRates?userId={userId}"))
			{
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<List<HourlyRate>>();
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task AddRate(HourlyRate model)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/HourlyRates/add", model))
			{
				if (response.IsSuccessStatusCode)
				{
					//TODO - Log successful post
					Console.WriteLine("Successfully Added Rate.");
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task EditRate(HourlyRate model)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/HourlyRates/edit", model))
			{
				if (response.IsSuccessStatusCode)
				{
					//TODO - Log successful post
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task<HourlyRate> GetByDate(DateTime date, string userId)
		{
			using (HttpResponseMessage response = await _client.GetAsync($"/api/HourlyRates/date?userId={userId}&Date={date}"))
			{
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<HourlyRate>();
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}
	}
}
