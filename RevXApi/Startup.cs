using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Common;
using RevXApi.Data;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Services;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RevXApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//CORS Policy
			services.AddCors(policy =>
			{
				policy.AddPolicy("OpenCorsPolicy", opt =>
				opt.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod());
			});

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("RevXApiAuthDb")));
			services.AddDatabaseDeveloperPageExceptionFilter();


			services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>();
			services.AddControllersWithViews();



			//Personal Services
			services.AddTransient<ISqlDataAccess, SqlDataAccess>();
			services.AddTransient<IUserData, UserData>();
			services.AddTransient<ISessionData, SessionData>();
			services.AddTransient<IStudentData, StudentData>();
			services.AddTransient<IProviderData, ProviderData>();
			services.AddTransient<IBillingStatusData, BillingStatusData>();
			services.AddTransient<IInvoiceData, InvoiceData>();
			services.AddTransient<IReportData, ReportData>();
			services.AddTransient<IHourlyRateData, HourlyRateData>();
			services.AddTransient<IUserData, UserData>();
			services.AddTransient<IPlayground, Playground>();

			// FluentEmail Configuration
			services.AddTransient<IEmailService, EmailService>();

			//services.AddFluentEmail(Configuration["EmailConfig:FaigyEmailAddress"], "Faigy Huss")
			//	.AddRazorRenderer()
			//	.AddSmtpSender(new SmtpClient("localhost", 25)
			//	{
			//		EnableSsl = false
			//	});

			// Gmail Setup
			services.AddFluentEmail(Configuration[ "EmailConfig:FaigyEmailAddress" ], "Faigy Huss")
				.AddRazorRenderer()
				.AddSmtpSender(new SmtpClient("smtp.gmail.com", 587)
				{
					Credentials = new NetworkCredential(Configuration[ "EmailConfig:FaigyEmailAddress" ], Configuration[ "EmailConfig:FaigyAppPassword" ]),
					EnableSsl = true
				});

			// JWT Authentication
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = "JwtBearer";
				options.DefaultChallengeScheme = "JwtBearer";
			})
				.AddJwtBearer("JwtBearer", jwtBearerOptions =>
				{
					jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySECreTKeYIsSEcReTSoDONtTeLl")),
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						ClockSkew = TimeSpan.FromMinutes(5)
					};
				});

			// Swagger
			services.AddSwaggerGen(setup =>
			{
				setup.SwaggerDoc(
				"v1",
				new OpenApiInfo
				{
					Title = "RevX API",
					Version = "v1"
				});
				// Security
				setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});
				setup.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						Array.Empty<string>()
					}
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			//// Make sure database is created
			//using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			//{
			//	scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.EnsureCreated();
			//}

			app.UseHttpsRedirection();
			app.UseCors("OpenCorsPolicy");
			app.UseStaticFiles();

			app.UseRouting();

			string logfileRoot = Configuration.GetValue<string>("nlog:LogFileRootDirectory");
			LogManager.Configuration.Variables[ "LogFileRootDirectory" ] = logfileRoot;
			InternalLogger.LogFile = Path.Combine(logfileRoot, "NlogInternal.log");
			InternalLogger.LogWriter = new StringWriter(); //e.g. TextWriter writer = File.CreateText("C:\\perl.txt")

			bool logAll = Configuration.GetValue<bool>("nlog:InternalLogAll");
			if (logAll)
			{
				InternalLogger.LogLevel = NLog.LogLevel.Trace;
			}
			else
			{
				InternalLogger.LogLevel = NLog.LogLevel.Info;
			}

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseSwagger();
			app.UseSwaggerUI(x =>
			{
				x.SwaggerEndpoint("/swagger/v1/swagger.json", "RevX API v1");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}
