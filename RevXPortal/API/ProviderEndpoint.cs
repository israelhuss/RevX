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

		public async Task<List<ProviderModel>> GetAll()
		{
			using (HttpResponseMessage response = await _client.GetAsync("/api/Provider"))
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
			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/Provider", model))
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
