using RevXPortal.Exceptions;
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
					throw new ApiException("The API cannot be reached");
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
			//return new WorkplaceModel();
		}
	}
}
