using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public class UserEndpoint : IUserEndpoint
	{
		private readonly HttpClient _client;

		public UserEndpoint(HttpClient client)
		{
			_client = client;
		}

		public async Task<string> GetCurrentUserId()
		{
			using (HttpResponseMessage response = await _client.GetAsync($"/api/User"))
			{
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<UserModel>();
					return result.Id;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task<UserModel> GetCurrentUser()
		{
			using (HttpResponseMessage response = await _client.GetAsync($"/api/User"))
			{
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<UserModel>();
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
