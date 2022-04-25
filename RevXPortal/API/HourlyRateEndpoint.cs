using RevXPortal.Exceptions;
using RevXPortal.Models;
using RevXPortal.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public class HourlyRateEndpoint : IHourlyRateEndpoint
	{
		private readonly HttpClient _client;
		private readonly IToastService _toastService;

		public HourlyRateEndpoint(HttpClient client, IToastService toastService)
		{
			_client = client;
			_toastService = toastService;
		}

		public async Task<List<HourlyRate>> GetAll()
		{
			try
			{
				using (HttpResponseMessage response = await _client.GetAsync($"/api/HourlyRates"))
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
			catch (HttpRequestException ex)
			{
				if (ex.Message == "TypeError: Failed to fetch")
				{
					throw new ApiException("The API cannot be reached");
				}
				else
				{
					throw;
				}
			}
			catch (Exception)
			{
				throw;
			}
			// return new List<HourlyRate>();
		}

		public async Task AddRate(HourlyRate model)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/HourlyRates", model))
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

		public async Task<HourlyRate> GetByDate(DateTime date)
		{
			using (HttpResponseMessage response = await _client.GetAsync($"/api/HourlyRates/date?Date={date}"))
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
