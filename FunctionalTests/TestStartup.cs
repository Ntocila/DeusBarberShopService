﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using deusbarbershop;
using Deus_DataAccessLayer.Data;
using Calzolari.TestServer.EntityFramework.Database.EF;
using Calzolari.TestServer.EntityFramework.FakeBearerToken;

namespace FunctionalTests
{
    public class TestStartup
    {
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddApplicationPart(typeof(Startup).Assembly);
            services.AddIntegrationTestDbContext<ApplicationDbContext>()
                    .AddFakeBearerToken();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}