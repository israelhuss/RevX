using RevXPortal.Models;
using RevXPortal.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public class ClientEndpoint : IClientEndpoint
	{
		private readonly HttpClient _client;
		private readonly IToastService _toastService;

		public ClientEndpoint(HttpClient client, IToastService toastService)
		{
			_client = client;
			_toastService = toastService;
		}

		public async Task<List<ClientModel>> GetAll()
		{
			try
			{
				using HttpResponseMessage response = await _client.GetAsync($"/api/Client");
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<List<ClientModel>>();
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
			catch (HttpRequestException ex)
			{
				if (ex.Message == "TypeError: Failed to fetch")
				{
					_toastService.ShowError("Looks like the API is offline.");
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
			return new List<ClientModel>();
		}

		public async Task<List<ClientModel>> GetEnabled()
		{
			using (HttpResponseMessage response = await _client.GetAsync($"/api/Client/enabled"))
			{
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<List<ClientModel>>();
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task Add(ClientModel model)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/Client/add", model))
			{
				if (response.IsSuccessStatusCode)
				{
					//TODO - Log successful post
					Console.WriteLine("Successfully Added Client.");
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task Edit(ClientModel model)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/Client/edit", model))
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
