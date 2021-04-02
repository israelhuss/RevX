using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public async Task<List<DisplaySessionModel>> GetAll()
		{
			using (HttpResponseMessage response = await _client.GetAsync("/api/Session"))
			{
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<List<DisplaySessionModel>>();
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task SaveSession(ManageSessionModel model)
		{
			SessionDbModel dbModel = new()
			{
				StudentId = model.Student.Id,
				Date = model.Date,
				StartTime = ConvertToTimeSpan(model.StartTime),
				EndTime = ConvertToTimeSpan(model.EndTime)

				//FINISH

			};

			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/Session", dbModel))
			{

			}
		}

		private TimeSpan ConvertToTimeSpan(string timeString)
		{
			var split = timeString.Split(':');
			int.TryParse(split[0], out int hours);
			int.TryParse(split[1], out int minutes);
			var output = new TimeSpan(hours, minutes, 00);
			return output;
		}
	}
}
