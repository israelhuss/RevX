using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RevXPortal.API;
using RevXPortal.Authentication;
using RevXPortal.Services;
using System;
using System.Net.Http;
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
			builder.Services.AddScoped<ToastService>();

			var baseAddress = builder.Configuration.GetValue<string>("apiLocation");
			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

			//Endpoints
			builder.Services.AddTransient<IStudentEndpoint, StudentEndpoint>();
			builder.Services.AddTransient<ISessionEndpoint, SessionEndpoint>();
			builder.Services.AddTransient<IInvoiceEndpoint, InvoiceEndpoint>();
			builder.Services.AddTransient<IProviderEndpoint, ProviderEndpoint>();
			builder.Services.AddTransient<IBillingStatusEndpoint, BillingStatusEndpoint>();
			builder.Services.AddTransient<IReportEndpoint, ReportEndpoint>();

			await builder.Build().RunAsync();
		}
	}
}
