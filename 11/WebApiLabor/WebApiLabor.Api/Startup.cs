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
using AutoMapper;
using WebApiLabor.Api.Mapping;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

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
            /*services.AddMvc().AddJsonOptions(
                json=>json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);*/

            services.AddMvc();
            services.AddTransient<IProductService, ProductService>();
            services.AddDbContext<NorthwindContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")
                 )
            );
            services.AddSingleton<IMapper>(MapperConfig.Configure());
            services.AddSwaggerGen(o =>
                    {
                        o.SwaggerDoc("v1", new Info { Title = "Labor API", Version = "v1" });
                        o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "WebApiLabor.Api.xml"));
                        o.DescribeAllEnumsAsStrings();
                    }
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

            app.UseSwagger();
            app.UseSwaggerUI(
                c=>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Labor API v1");
                }
            );

            //MIGRÁCIÓT ELRONTJA
            //northwindContext.Seed();
        }
    }
}
