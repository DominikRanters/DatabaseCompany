﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chayns.Auth.ApiExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using CompanyAPI.Repository;
using CompanyAPI.Helper;
using CompanyAPI.Middleware;

namespace CompanyAPI
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
            services.AddChaynsAuth();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<DbSettings>(Configuration.GetSection("DbSettings"));

            services.AddScoped<IBaseInterface<Company, CompanyDto>, CompanyRepository>();
            services.AddScoped<IBaseInterface<Departmnet, DepartmentDto>, DepartmentRepository>();
            services.AddScoped<IBaseInterface<Employee, EmployeeDto>, EmployeeRepository>();

            services.AddSingleton<IDbContext, DbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRepoExceptionMiddleware();
            //app.UseAuthorizationMiddleware();
            app.InitChaynsAuth();
            app.UseMvc();
        }
    }
}
