using Data.Extensions.DI;
using Data.Models.Configurations;
using Data.Services.DB;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Accelerator
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
            #region Базовая инициализация DI

            services.AddBaseModuleDI(Configuration.GetConnectionString("DefaultConnection"));

            #endregion


            services.Configure<PathConfig>(Configuration.GetSection("PathConfig"));

            #region Hangfire

            // Add Hangfire services
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(Configuration.GetConnectionString("HangfireConnection"), new PostgreSqlStorageOptions
                {
                    DistributedLockTimeout = TimeSpan.FromMinutes(1)
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer(options =>
            {
                options.WorkerCount = 5;
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IApiVersionDescriptionProvider provider,
            IWebHostEnvironment env,
            InitDB InitDB)
        {
            app.UseBaseServices(env, provider);

            #region Init


            #endregion
        }
    }
}