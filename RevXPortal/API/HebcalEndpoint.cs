using Microsoft.JSInterop;
using System.Net;
using System.Net.Http.Headers;

namespace RevXPortal.API
{
	public class HebcalEndpoint : IHebcalEndpoint
	{
		private readonly HttpClient _client;
		private readonly IJSRuntime _jSRuntime;

		public HebcalEndpoint(HttpClient client, IJSRuntime jSRuntime)
		{
			_client = client;
			_jSRuntime = jSRuntime;
		}

		record HebcalResponse
		{
			public string title { get; set; }
			public DateTime date { get; set; }
			public Location location { get; set; }
			public HEvent[] items { get; set; }
		}

		record Location
		{
			public string geo { get; set; }
		}

		record HEvent
		{
			public string title { get; set; }
			public string date { get; set; }
			public string category { get; set; }
			public string subcat { get; set; }
			public string hebrew { get; set; }
			public Leyning leyning { get; set; }
			public string link { get; set; }
			public string memo { get; set; }
			public bool yomtov { get; set; }
		}

		public class Leyning
		{
			public string _7 { get; set; }
			public string haftarah { get; set; }
			public string maftir { get; set; }
			public string _1 { get; set; }
			public string _2 { get; set; }
			public string _3 { get; set; }
			public string torah { get; set; }
			public string _4 { get; set; }
			public string _5 { get; set; }
			public string _6 { get; set; }
		}

		public async Task<List<HebcalEvent>> GetHebcalEvents(int month, int year)
		{
			AuthenticationHeaderValue oldHeader = _client.DefaultRequestHeaders.Authorization;
			try
			{
				_client.DefaultRequestHeaders.Authorization = null;
				using HttpResponseMessage response = await _client.GetAsync($"https://www.hebcal.com/hebcal?v=1&cfg=json&maj=on&min=on&nx=on&year={year}&month={month}&ss=on&mf=on");
				if (response.IsSuccessStatusCode)
				{
					HebcalResponse result = await response.Content.ReadAsAsync<HebcalResponse>();
					List<HebcalEvent> list = new();
					foreach (var item in result.items)
					{
						HebcalEvent hebcalEvent = new()
						{
							Date = DateOnly.Parse(item.date),
							AllDay = true,
							Title = item.hebrew,
						};
						list.Add(hebcalEvent);
					}
					_client.DefaultRequestHeaders.Authorization = oldHeader;
					return list;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
			catch (Exception ex)
			{
				_client.DefaultRequestHeaders.Authorization = oldHeader;
				await _jSRuntime.InvokeVoidAsync("log", ex.Message);
				return new();
			}
		}
	}
}
