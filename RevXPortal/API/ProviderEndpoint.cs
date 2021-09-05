using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public class ProviderEndpoint : IProviderEndpoint
	{
		private readonly HttpClient _client;

		public ProviderEndpoint(HttpClient client)
		{
			_client = client;
		}

		public async Task<List<ProviderModel>> GetAll(string userId)
		{
			using (HttpResponseMessage response = await _client.GetAsync($"/api/Provider?userId={userId}"))
			{
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<List<ProviderModel>>();
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task<List<ProviderModel>> GetEnabled(string userId)
		{
			using (HttpResponseMessage response = await _client.GetAsync($"/api/Provider/enabled?userId={userId}"))
			{
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<List<ProviderModel>>();
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task AddProvider(ProviderModel model)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/Provider/add", model))
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

		public async Task EditProvider(ProviderModel model)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/Provider/edit", model))
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
	}
}
