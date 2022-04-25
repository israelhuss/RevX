using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Web;
using RevXPortal.Authentication;
using RevXPortal.Services;
using Microsoft.Extensions.Logging;
using Blazored.Toast;
using System.Text.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
//builder.Services.AddScoped<IToastService, ToastService>();
builder.Services.AddBlazoredToast();
builder.Services.Configure<JsonSerializerOptions>(opt =>
{
	opt.Converters.Add(new DateOnlyConverter());
	opt.Converters.Add(new TimeOnlyConverter());
});

builder.Logging.SetMinimumLevel(LogLevel.Warning);

var baseAddress = builder.Configuration.GetValue<string>("apiLocation");
builder.Services.AddScoped<HttpClient, HttpClient>(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });


//Endpoints
builder.Services.AddTransient<IClientEndpoint, ClientEndpoint>();
builder.Services.AddTransient<ISessionEndpoint, SessionEndpoint>();
builder.Services.AddTransient<IInvoiceEndpoint, InvoiceEndpoint>();
builder.Services.AddTransient<IProviderEndpoint, ProviderEndpoint>();
builder.Services.AddTransient<IBillingStatusEndpoint, BillingStatusEndpoint>();
builder.Services.AddTransient<IReportEndpoint, ReportEndpoint>();
builder.Services.AddTransient<IHourlyRateEndpoint, HourlyRateEndpoint>();
builder.Services.AddTransient<IUserEndpoint, UserEndpoint>();
builder.Services.AddTransient<IWorkplaceEndpoint, WorkplaceEndpoint>();
builder.Services.AddTransient<IHebcalEndpoint, HebcalEndpoint>();
builder.Services.AddTransient<IPendingSessionEndpoint, PendingSessionEndpoint>();



await builder.Build().RunAsync();


//namespace RevXPortal
//{
//	public class Program
//	{
//		public static async Task Main(string[] args)
//		{
//			var builder = WebAssemblyHostBuilder.CreateDefault(args);
//			builder.RootComponents.Add<App>("#app");
//			builder.RootComponents.Add<HeadOutlet>("head::after");

//			builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
//			builder.Services.AddBlazoredLocalStorage();
//			builder.Services.AddAuthorizationCore();
//			builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
//			builder.Services.AddScoped<ToastService>();

//			builder.Logging.SetMinimumLevel(LogLevel.Warning);

//			var baseAddress = builder.Configuration.GetValue<string>("apiLocation");
//			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

//			//Endpoints
//			builder.Services.AddTransient<IClientEndpoint, ClientEndpoint>();
//			builder.Services.AddTransient<ISessionEndpoint, SessionEndpoint>();
//			builder.Services.AddTransient<IInvoiceEndpoint, InvoiceEndpoint>();
//			builder.Services.AddTransient<IProviderEndpoint, ProviderEndpoint>();
//			builder.Services.AddTransient<IBillingStatusEndpoint, BillingStatusEndpoint>();
//			builder.Services.AddTransient<IReportEndpoint, ReportEndpoint>();
//			builder.Services.AddTransient<IHourlyRateEndpoint, HourlyRateEndpoint>();
//			builder.Services.AddTransient<IUserEndpoint, UserEndpoint>();

//			await builder.Build().RunAsync();
//		}
//	}
//}


