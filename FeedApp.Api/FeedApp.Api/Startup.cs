﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FeedApp.Api.Mapping;
using FeedApp.Bll.Context;
using FeedApp.Bll.Entities;
using FeedApp.Bll.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FeedApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }

        //To redirect HTTP requests to HTTPS:
        //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/url-rewriting?view=aspnetcore-2.0&tabs=aspnetcore2x
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, ApplicationRole>()
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();

            /*var skipHTTPS = Configuration.GetValue<bool>("LocalTest:skipHTTPS");
            services.Configure<MvcOptions>(options =>
            {
                if (Environment.IsDevelopment() && !skipHTTPS)
                {
                    options.Filters.Add(new RequireHttpsAttribute());
                }
            });*/

            services.AddTransient<IFoodService, FoodService>();
            services.AddTransient<IEatingService, EatingService>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<IUserInfoService, UserInfoService>();
            services.AddTransient<IDailyWeightInfoService, DailyWeightInfoService>();
            services.AddSingleton<IMapper>(MapperConfig.Configure());

            services.AddAuthorization(options =>
            {
                options.AddPolicy("GuruOnly", policy => policy.RequireClaim("Level", "Guru"));
            });

            services.AddMvc()
                .AddJsonOptions(json => json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            /*services.AddSingleton<IEmailSenderService, EmailSenderService>();
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();



            //context.Seed();
        }
    }
}
