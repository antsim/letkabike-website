﻿using System.Reflection;
using System.Threading.Tasks;
using LetkaBike.Core.Data;
using LetkaBike.Core.Handlers.Cities;
using LetkaBike.Core.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace LetkaBike.API
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

            // Use Sql Server 
            //services.AddDbContext<LetkaContext>(options =>
            //   options.UseSqlServer(Configuration.GetConnectionString("LetkaDatabase")));

            // or 

            // Sqlite
            //services.AddDbContext<LetkaContext> (options =>
            //    options.UseSqlite("DataSource=:memory:"));

            // or

            // SqlServer in-memory
            services.AddDbContext<LetkaContext>(options =>
                options.UseInMemoryDatabase("Letka"));

            services.AddDefaultIdentity<Rider>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<LetkaContext>()
                .AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            services.AddMediatR(Assembly.GetAssembly(typeof(GetCitiesHandler)));
            
            services.TryAddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Automatically ensure the DB is created
                using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
                var context = serviceScope.ServiceProvider.GetRequiredService<LetkaContext>();
                context.Database.EnsureCreated();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }
    }
}