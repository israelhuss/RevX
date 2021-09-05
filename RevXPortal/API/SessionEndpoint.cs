using RevXPortal.Converters;
using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public class SessionEndpoint : ISessionEndpoint
	{
		private readonly HttpClient _client;

		public SessionEndpoint(HttpClient client)
		{
			_client = client;
		}

		public async Task<List<ManageSessionModel>> GetAll(string userId)
		{
			using (HttpResponseMessage response = await _client.GetAsync($"/api/Session?userId={userId}"))
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
							Student = result.Student,
							Date = result.Date,
							StartTime = TimeConverters.Convert24HourStringToTimeSpan(result.StartTime),
							EndTime = TimeConverters.Convert24HourStringToTimeSpan(result.EndTime),
							Provider = result.Provider,
							BillingStatus = result.BillingStatus,
							Notes = result.Notes
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

		public async Task SaveSession(ManageSessionModel model)
		{
			//SessionDbModel dbModel = new()
			//{
			//	StudentId = model.Student.Id,
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

		public async Task DeleteSession(int id, string userId)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync($"/api/session/delete/{id}?userId={userId}", new { }))
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
