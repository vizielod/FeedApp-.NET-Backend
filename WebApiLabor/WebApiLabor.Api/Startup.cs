using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiLabor.Bll.Context;
using WebApiLabor.Bll.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WebApiLabor.Api
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
            services.AddMvc().AddJsonOptions(
                json=>json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);            
            services.AddTransient<IProductService, ProductService>();
            services.AddDbContext<NorthwindContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")
                 )
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env
            , NorthwindContext northwindContext)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //MIGRÁCIÓT ELRONTJA
            northwindContext.Seed();
        }
    }
}
