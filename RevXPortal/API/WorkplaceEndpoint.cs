using RevXPortal.Services;

namespace RevXPortal.API
{
	public class WorkplaceEndpoint : IWorkplaceEndpoint
	{
		private readonly HttpClient _client;
		private readonly IToastService _toastService;

		public WorkplaceEndpoint(HttpClient client, IToastService toastService)
		{
			_client = client;
			_toastService = toastService;
		}

		public async Task<WorkplaceModel> GetMyWorkplaceInfo()
		{
			try
			{
				using (HttpResponseMessage response = await _client.GetAsync($"/api/Workplace/GetMyInfo"))
				{
					if (response.IsSuccessStatusCode)
					{
						var result = await response.Content.ReadAsAsync<WorkplaceModel>();
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
			return new WorkplaceModel();
		}
	}
}
