using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public class StudentEndpoint : IStudentEndpoint
	{
		private readonly HttpClient _client;

		public StudentEndpoint(HttpClient client)
		{
			_client = client;
		}

		public async Task<List<StudentModel>> GetAll(string userId)
		{
			using HttpResponseMessage response = await _client.GetAsync($"/api/Student?userId={userId}");
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

		public async Task<List<StudentModel>> GetEnabled(string userId)
		{
			using (HttpResponseMessage response = await _client.GetAsync($"/api/Student/enabled?userId={userId}"))
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
