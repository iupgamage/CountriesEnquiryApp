using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountriesEnquiryApp.BAL.Interfaces;
using CountriesEnquiryApp.BAL.Services;
using CountriesEnquiryApp.Common.Enums;
using CountriesEnquiryApp.Common.Interfaces;
using CountriesEnquiryApp.Common.Mapper;
using CountriesEnquiryApp.Common.Services;
using CountriesEnquiryApp.DAL.Interfaces;
using CountriesEnquiryApp.DAL.Services;
using CountriesEnquiryApp.Messaging.Interfaces;
using CountriesEnquiryApp.Messaging.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CountriesEnquiryApp.API
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
            services.AddControllers();

            services.AddSwaggerGen();

            services.AddHttpClient();

            services.AddHttpContextAccessor();

            services.AddAutoMapper(typeof(AutoMapping));

            services.AddScoped<IEnquiriesBusinessService, EnquiriesBusinessService>();

            services.AddScoped<IEnquiriesDataService, EnquiriesDataService>();

            services.AddScoped<IContextAccessor, ContextAccessor>();

            services.AddSingleton<IServiceBusMessageSender, ServiceBusMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                context.Items.Add(RequestContext.RequestMadeAt, DateTime.Now);
                await next();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "CountriesEnquiryApp");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
