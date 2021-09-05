using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public class BillingStatusEndpoint : IBillingStatusEndpoint
	{
		private readonly HttpClient _client;

		private List<BillingStatusModel> _billingStatuses;

		public List<BillingStatusModel> BillingStatuses
		{
			get { return _billingStatuses; }
			set { _billingStatuses = value; }
		}

		public BillingStatusEndpoint(HttpClient client)
		{
			_client = client;
		}

		public async Task<List<BillingStatusModel>> GetAll()
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
