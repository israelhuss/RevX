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
					_toastService.ShowToast("Looks like the API is offline.", ToastLevel.Error);
					throw;
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
			return new WorkplaceModel();
		}
	}
}
