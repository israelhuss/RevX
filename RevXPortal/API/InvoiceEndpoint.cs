using Microsoft.JSInterop;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Asn1.Ocsp;
using RevXPortal.Models;
using RevXPortal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public class InvoiceEndpoint : IInvoiceEndpoint
	{
		private readonly HttpClient _client;
		private readonly IJSRuntime jSRuntime;
		private readonly IToastService _toastService;

		public InvoiceEndpoint(HttpClient client, IJSRuntime jSRuntime, IToastService toastService)
		{
			_client = client;
			this.jSRuntime = jSRuntime;
			_toastService = toastService;
		}

		public async Task<List<InvoiceModel>> GetAll()
		{
			try
			{
				using (HttpResponseMessage response = await _client.GetAsync($"/api/Invoice"))
				{
					if (response.IsSuccessStatusCode)
					{
						var result = await response.Content.ReadAsAsync<List<InvoiceModel>>();
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
			return new List<InvoiceModel>();
		}

		public async Task SaveInvoice(InvoiceModel invoice)
		{
			using (HttpResponseMessage response = await _client.PostAsJsonAsync("/api/invoice", invoice))
			{
				Console.WriteLine(response.StatusCode);
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Invoice was saved to the database.");
				}
				else
				{
					Console.WriteLine(response.StatusCode);
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task GetDocument(int id, string filename)
		{
			using (var request = new HttpRequestMessage(HttpMethod.Get, $"/api/invoice/document?id={id}"))
			{
				using (Stream contentStream = await ( await _client.SendAsync(request) ).Content.ReadAsStreamAsync())
				{
					using MemoryStream stream = new();
					await contentStream.CopyToAsync(stream);
					byte[] bytes = stream.ToArray();
					await jSRuntime.InvokeVoidAsync("BlazorDownloadFile", $"{filename}.pdf", "application/pdf", bytes);
				}
			}
		}
	}
}
