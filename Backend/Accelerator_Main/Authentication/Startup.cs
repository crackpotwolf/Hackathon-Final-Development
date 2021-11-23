﻿using Data.Extensions.DI;
using Data.Services.DB;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Authentication
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider,
            InitDB initService)
        {
            app.UseBaseServices(env, provider);

            #region Init

            initService.InitAuth();

            #endregion
        }
    }
}