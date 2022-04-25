using RevXPortal.Exceptions;
using RevXPortal.Services;

namespace RevXPortal.API
{
	public class SessionEndpoint : ISessionEndpoint
	{
		private readonly HttpClient _client;
		private readonly IToastService _toastService;

		public SessionEndpoint(HttpClient client, IToastService toastService)
		{
			_client = client;
			_toastService = toastService;
		}

		public async Task<List<ManageSessionModel>> GetAll()
		{
			try
			{
				using (HttpResponseMessage response = await _client.GetAsync($"/api/Session"))
				{
					if (response.IsSuccessStatusCode)
					{
						var resultsAsDbModel = await response.Content.ReadAsAsync<List<SessionApiModel>>();
						var output = new List<ManageSessionModel>();
						foreach (var result in resultsAsDbModel)
						{
							ManageSessionModel model = new()
							{
								Id = result.Id,
								UserId = result.UserId,
								Client = result.Client,
								Date = result.Date,
								StartTime = TimeConverters.Convert24HourStringToTimeSpan(result.StartTime),
								EndTime = TimeConverters.Convert24HourStringToTimeSpan(result.EndTime),
								Provider = result.Provider,
								BillingStatus = result.BillingStatus,
								Notes = result.Notes,
								Rate = result.Rate,
								InvoiceId = result.InvoiceId
							};

							output.Add(model);
						}
						return output;
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
					throw;
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
			//return new List<ManageSessionModel>();
		}

		public async Task SaveSession(ManageSessionModel model)
		{
			//SessionDbModel dbModel = new()
			//{
			//	ClientId = model.Client.Id,
			//	Date = model.Date,
			//	StartTime = ConvertToTimeSpan(model.StartTime),
			//	EndTime = ConvertToTimeSpan(model.EndTime),
			//	ProviderId = model.Provider.Id,
			//	BillingStatusId = model.BillingStatus.Id,
			//	Notes = model.Notes
			//};

			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/Session", model))
			{
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Session was saved to the database.");
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task EditSession(ManageSessionModel model)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/session/edit", model))
			{
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Session was successfully updated.");
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task DeleteSession(int id)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync($"/api/session/delete/{id}", new { }))
			{
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Session was successfully deleted.");
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}
	}
}
