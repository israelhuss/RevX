using RevXPortal.Models;
using RevXPortal.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public class BillingStatusEndpoint : IBillingStatusEndpoint
	{
		private readonly HttpClient _client;
		private readonly IToastService _toastService;
		private List<BillingStatusModel> _billingStatuses;

		public List<BillingStatusModel> BillingStatuses
		{
			get { return _billingStatuses; }
			set { _billingStatuses = value; }
		}

		public BillingStatusEndpoint(HttpClient client, IToastService toastService)
		{
			_client = client;
			_toastService = toastService;
		}

		public async Task<List<BillingStatusModel>> GetAll()
		{
			try
			{
				using (HttpResponseMessage response = await _client.GetAsync("/api/BillingStatus"))
				{
					if (response.IsSuccessStatusCode)
					{
						var result = await response.Content.ReadAsAsync<List<BillingStatusModel>>();
						BillingStatuses = result;
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
					_toastService.ShowToast("Looks like the API is offline.", ToastLevel.Error);
				}
				else
				{
					throw;
				}
			}
			catch (Exception)
			{
				_toastService.ShowToast("An unexpected error ocurred.", ToastLevel.Error);
			}
			return new List<BillingStatusModel>();
		}

		public async Task<List<BillingStatusModel>> GetEnabled()
		{
			using (HttpResponseMessage response = await _client.GetAsync($"/api/BillingStatus/enabled"))
			{
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<List<BillingStatusModel>>();
					BillingStatuses = result;
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task AddBillingStatus(BillingStatusModel model)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/BillingStatus/add", model))
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

		public async Task EditBillingStatus(BillingStatusModel model)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/BillingStatus/edit", model))
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
