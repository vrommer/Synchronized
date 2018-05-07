using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synchronized.Repository.Repositories;
using Synchronized.Core.Repositories;
using Synchronized.Model;
using Synchronized.Repository;
using Synchronized.Core;
using Synchronized.Data;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using StructureMap;
using Synchronized.Core.Infrastructure;
using Synchronized.Repository.Infrastructure;

namespace Synchronized.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddUserStore<UsersRepository>()
                .AddRoleStore<RolesRepository>()
                .AddDefaultTokenProviders();

            services.AddDbContext<SynchronizedDbContext>(b =>
            {
                b.UseSqlServer(@"Server = (localdb)\mssqllocaldb; Database = SynchronizedData; Trusted_Connection = true");
            });

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                })
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });


            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            //services.AddSingleton<IEmailSender, EmailSender>();

            return ConfigureIoC(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseNodeModules(env.ContentRootPath);

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "api",
                    template: "api/{controller}/{action}");
            });
        }
        
        private IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            var registry = new Registry();
            registry.IncludeRegistry<ServiceRegistry>();
            registry.IncludeRegistry<RepositoryRegistry>();

            var container = new Container(registry);

            container.Configure(_ =>
            {
                _.Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.AssemblyContainingType<QuestionsRepository>();
                    x.AssemblyContainingType<QuestionsService>();
                    x.WithDefaultConventions();
                });
                _.Populate(services);
            });
            return container.GetInstance<IServiceProvider>();
        }
    }
}
