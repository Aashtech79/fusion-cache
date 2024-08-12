
using Fusion_Cache_Lib.Services.Fusion_Cache;
using Microsoft.AspNetCore.Mvc;
using NetCore.AutoRegisterDi;
using System.Reflection;
using ZiggyCreatures.Caching.Fusion;

namespace fusion_cache
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			
			builder.Services.AddFusionCache().TryWithAutoSetup();

			// Configure API versioning
			builder.Services.AddApiVersioning(options =>
			{
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ReportApiVersions = true;
			});

			var assemble = Assembly.Load("Fusion Cache_Lib");
			// Manually register the FusionCacheService (if it's a custom implementation)
			builder.Services.AddSingleton<IFusionCacheHelper, FusionCacheHelper>();
			// Register services using AutoRegisterDi
			builder.Services.RegisterAssemblyPublicNonGenericClasses(AppDomain.CurrentDomain.GetAssemblies())
				.Where(x => x.Name.EndsWith("Service") && x.Name != "FusionCacheService")
				.AsPublicImplementedInterfaces(ServiceLifetime.Scoped);


			// Configure named HttpClient instances with specific base addresses
			builder.Services.AddHttpClient("dummyapi", client =>
			{
				client.BaseAddress = new Uri("https://dummy.restapiexample.com/api/v1/");
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
