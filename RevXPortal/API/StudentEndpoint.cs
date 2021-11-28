using RevXPortal.Models;
using RevXPortal.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public class StudentEndpoint : IStudentEndpoint
	{
		private readonly HttpClient _client;
		private readonly IToastService _toastService;

		public StudentEndpoint(HttpClient client, IToastService toastService)
		{
			_client = client;
			_toastService = toastService;
		}

		public async Task<List<StudentModel>> GetAll()
		{
			try
			{
				using HttpResponseMessage response = await _client.GetAsync($"/api/Student");
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<List<StudentModel>>();
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
			catch (HttpRequestException ex)
			{
				if (ex.Message == "TypeError: Failed to fetch")
				{
					_toastService.ShowToast("Looks like the API is offline.", ToastLevel.Error);
				}
				else
				{
					throw;
				}
			}
			catch (Exception ex)
			{
				_toastService.ShowToast("An unexpected error ocurred.", ToastLevel.Error);
			}
			return new List<StudentModel>();
		}

		public async Task<List<StudentModel>> GetEnabled()
		{
			using (HttpResponseMessage response = await _client.GetAsync($"/api/Student/enabled"))
			{
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<List<StudentModel>>();
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task AddStudent(StudentModel model)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/Student/add", model))
			{
				if (response.IsSuccessStatusCode)
				{
					//TODO - Log successful post
					Console.WriteLine("Successfully Added Student.");
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task EditStudent(StudentModel model)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/Student/edit", model))
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
