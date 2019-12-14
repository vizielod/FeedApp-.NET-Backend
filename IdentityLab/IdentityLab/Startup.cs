using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IdentityLab.Entities;
using Microsoft.AspNetCore.Identity;
using IdentityLab.Identity;

namespace IdentityLab
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
            services.AddDbContext<ApplicationDbContext>(
                o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                );
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<ApplicationUser>(); //Applicationuser lesz az osztalya az identity-s modellunknek. itt kapcsolodik ossze a ketto

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(
                    options =>
                    {
                        options.Authority = "http://lovalhost:PORT";
                        options.RequireHttpsMetadata = false;
                        options.ApiName = "api1";
                    }

                 );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer(); //middleware

            app.UseMvc();
        }
    }
}
