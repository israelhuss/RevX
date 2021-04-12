using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RevXPortal.API;
using RevXPortal.Authentication;
using RevXPortal.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RevXPortal
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
			builder.Services.AddBlazoredLocalStorage();
			builder.Services.AddAuthorizationCore();
			builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44300/") });

			//Endpoints
			builder.Services.AddTransient<IStudentEndpoint, StudentEndpoint>();
			builder.Services.AddTransient<ISessionEndpoint, SessionEndpoint>();

			//Services
			builder.Services.AddTransient<IPdfService, PdfService>();

			await builder.Build().RunAsync();
		}
	}
}
