using RevXPortal.Models;
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

		public InvoiceEndpoint(HttpClient client)
		{
			_client = client;
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
	}
}
