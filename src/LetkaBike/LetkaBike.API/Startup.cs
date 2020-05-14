using System;
using System.Reflection;
using System.Threading.Tasks;
using LetkaBike.API.Behaviors;
using LetkaBike.API.Configuration;
using LetkaBike.Core.Data;
using LetkaBike.Core.Handlers.Cities;
using LetkaBike.Core.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using VueCliMiddleware;
    
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
            services.Configure<AppOptions>(Configuration.GetSection("AppOptions"));

            var useDb = Configuration.GetSection("AppOptions").GetValue<string>("UseDB");

            switch (Enum.Parse<UseDb>(useDb))
            {
                case UseDb.Sqlite:
                    services.AddDbContext<LetkaContext> (options =>
                        options.UseSqlite("DataSource=:memory:"));
                    break;
                case UseDb.InMemory:
                    services.AddDbContext<LetkaContext>(options =>
                        options.UseInMemoryDatabase("Letka"));
                    break;
                case UseDb.SqlServer:
                    services.AddDbContext<LetkaContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("LetkaDatabase")));
                    break;
                case UseDb.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            services.AddDefaultIdentity<Rider>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<LetkaContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<Rider, LetkaContext>();
            
            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.TryAddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddMediatR(Assembly.GetAssembly(typeof(GetCitiesHandler)));

            services.TryAddScoped<IUnitOfWork, UnitOfWork>();
            
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
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
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                if (env.IsDevelopment())
                {
                    
                    endpoints.MapToVueCliProxy(
                        "{*path}",
                        new SpaOptions { SourcePath = "ClientApp" },
                        npmScript: "serve",
                        regex: "Compiled successfully");
                }
            });
            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                
            });
        }
    }
}