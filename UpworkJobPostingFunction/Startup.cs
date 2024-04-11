using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Jobson.Models;
using Jobson.Repositories;
using Jobson.Services;
using Jobson.ServicesMock;

[assembly: FunctionsStartup(typeof(Jobson.Startup))]

namespace Jobson
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Worker", LogEventLevel.Warning)
                .MinimumLevel.Override("Host", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Error)
                .MinimumLevel.Override("Function", LogEventLevel.Error)
                .MinimumLevel.Override("Azure.Storage.Blobs", LogEventLevel.Error)
                .MinimumLevel.Override("Azure.Core", LogEventLevel.Error)
                .Enrich.WithProperty("Application", $"xxxxx.AzureFunctions.{builder.GetContext().EnvironmentName}")
                .Enrich.FromLogContext()
                .WriteTo.Console(theme: SystemConsoleTheme.Literate)
                .CreateLogger();

            builder.Services.AddLogging(lb =>
            {
                lb.AddSerilog(Log.Logger, true);
            });

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddHttpClient();

            #region Debugging 
            //If the Enviorment is Development then add the following services
            if (builder.GetContext().EnvironmentName == "Development")
            {
                ///
                // Register services
                ///
                builder.Services.AddScoped<IJobFilterService, JobFilterService>();
                builder.Services.AddScoped<ICoverletterService, CoverLetterService>();
                builder.Services.AddScoped<IPromptRepository, PromptRepository>();
                builder.Services.AddScoped<IUpworkGraphQLService, MockUpworkGraphQLService>();
                builder.Services.AddScoped<IProcessFeedService, ProcessFeedService>();
                builder.Services.AddScoped<IUpworkProfileService, UpworkProfileService>();
                ///
                //Repositories
                ///
                builder.Services.AddScoped<IUpworkProfileUrlRepository, UpworkProfileUrlRepository>();
                builder.Services.AddScoped<IJobRepository, JobRepository>();
            }
            else
            {
                ///
                // Register services
                ///
                builder.Services.AddScoped<IJobFilterService, JobFilterService>();
                builder.Services.AddScoped<ICoverletterService, CoverLetterService>();
                builder.Services.AddScoped<IPromptRepository, PromptRepository>();
                builder.Services.AddScoped<IUpworkGraphQLService, UpworkGraphQLService>();
                builder.Services.AddScoped<IProcessFeedService, ProcessFeedService>();
                builder.Services.AddScoped<IUpworkProfileService, UpworkProfileService>();
                ///
                //Repositories
                ///
                builder.Services.AddScoped<IUpworkProfileUrlRepository, UpworkProfileUrlRepository>();
                builder.Services.AddScoped<IJobRepository, JobRepository>();
            }

            #endregion


            
            // Populate the AppSettings class with Anthropic_Key
            builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

        }

    }
}