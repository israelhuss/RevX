using RevXPortal.Models;
using RevXPortal.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public class ProviderEndpoint : IProviderEndpoint
	{
		private readonly HttpClient _client;
		private readonly IToastService _toastService;

		public ProviderEndpoint(HttpClient client, IToastService toastService)
		{
			_client = client;
			_toastService = toastService;
		}

		public async Task<List<ProviderModel>> GetAll()
		{
			try
			{
				using (HttpResponseMessage response = await _client.GetAsync($"/api/Provider"))
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
			catch (HttpRequestException ex)
			{
				if (ex.Message == "TypeError: Failed to fetch")
				{
					_toastService.ShowError("Looks like the API is offline.");
					throw;
				}
				else
				{
					throw;
				}
			}
			catch (Exception)
			{
				_toastService.ShowError("An unexpected error ocurred.");
			}
			return new List<ProviderModel>();
		}

		public async Task<List<ProviderModel>> GetEnabled()
		{
			using (HttpResponseMessage response = await _client.GetAsync($"/api/Provider/enabled"))
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
