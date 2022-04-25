using System.Text;
using System.Text.Json;

namespace RevXPortal.API
{
	public class PendingSessionEndpoint : IPendingSessionEndpoint
	{
		private readonly HttpClient _client;
		private readonly IToastService _toastService;

		public PendingSessionEndpoint(HttpClient client, IToastService toastService)
		{
			_client = client;
			_toastService = toastService;
		}

		public async Task<List<SessionApiModel>> GetByMonth(int month, int year)
		{
			try
			{
				using HttpResponseMessage response = await _client.GetAsync($"/api/PendingSession?month={month}&year={year}");
				if (response.IsSuccessStatusCode)
				{
					List<SessionApiModel> result = await response.Content.ReadAsAsync<List<SessionApiModel>>();
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
			catch (Exception ex)
			{
				return new();
			}
		}

		public async Task AddSchedule(ScheduleModel schedule)
		{
			try
			{
				string json = JsonSerializer.Serialize(schedule);
				StringContent requestContent = new(json, Encoding.UTF8, "application/json");
				using HttpResponseMessage response = await _client.PostAsync($"/api/PendingSession", requestContent);
				if (response.IsSuccessStatusCode)
				{
					_toastService.ShowSuccess("Schedule was saved!");
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
			catch (Exception ex)
			{
				_toastService.ShowError("Error saving schedule :(");
			}
		}

		public async Task Delete(int id)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync($"/api/PendingSession/delete/{id}", new { }))
			{
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Pending session was successfully deleted.");
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}
	}
}
